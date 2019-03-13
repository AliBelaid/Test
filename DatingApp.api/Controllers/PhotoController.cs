using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.api.Data;
using DatingApp.api.DTO;
using DatingApp.api.Helpers;
using DatingApp.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DatingApp.api.Controllers {

    [Route ("api/user/{UserId}/photo")]
    [ApiController]

    public class PhotoController : ControllerBase {

        public IOptions<cloudinaryDto> _cloud { get; }
        public IUserRepository _repo { get; set; }
        public IMapper _mapper { get; set; }
        public Cloudinary _cloudinary;

        public PhotoController (IOptions<cloudinaryDto> cloud,
            IUserRepository Repo, IMapper mapper) {
            _repo = Repo;
            _cloud = cloud;
            _mapper = mapper;

            Account acc = new Account (
                _cloud.Value.CloudName,
                _cloud.Value.ApiKey,
                _cloud.Value.ApiSecret);

            _cloudinary = new Cloudinary (acc);
        }

        [HttpGet ("{id}", Name = "GetPhoto")]
        public async Task<ActionResult> GetPhoto (int id) {
            var photo = await _repo.GetPhoto (id);
            var photoReturenDto = _mapper.Map<PhotoReturenDto> (photo);

            return Ok (photoReturenDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddPhotoForUser (int UserId, [FromForm] PhotoCreationeDto photoUpdateDto) {
            if (UserId != int.Parse (User.FindFirst (ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized ();
            }
            var UserUpload = await _repo.GetUser (UserId);
            var file = photoUpdateDto.File;
            var imgUpload = new ImageUploadResult ();
            if (file.Length > 0) {

                using (var stream = file.OpenReadStream ()) {
                    var uploadStream = new ImageUploadParams () {
                    File = new FileDescription (file.Name, stream), Transformation = new Transformation ()
                    .Width (500).Height (500).Crop ("fill").Gravity ("face")
                    };
                    imgUpload = _cloudinary.Upload (uploadStream);
                }
            }
            photoUpdateDto.Url = imgUpload.Uri.ToString ();
            photoUpdateDto.PhotoId = imgUpload.PublicId;

            var photo = _mapper.Map<Photo> (photoUpdateDto);
            if (!UserUpload.Photos.Any (m => m.isMain)) {
                photo.isMain = true;
            }
            UserUpload.Photos.Add (photo);

            if (await _repo.SaveAll ()) {
                var photoReturen = _mapper.Map<PhotoReturenDto> (photo);
                return CreatedAtRoute ("GetPhoto", new { id = photo.Id }, photoReturen);
            }
            return BadRequest ("Could not add the  photo");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto (int Userid, int id) {

            if (Userid != int.Parse (User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized ();
            }
            var userPhtoMain = await _repo.GetUser(Userid);

              if (!userPhtoMain.Photos.Any(p => p.Id== id)) {
                return Unauthorized ();
            }
            var newMainPhoto = await _repo.GetPhoto(id);
            if (newMainPhoto.isMain)
                return BadRequest ("the Photo is already the Main");

          
            var CurrncyPhtoMain = await _repo.GetMainPhoto(Userid);
            CurrncyPhtoMain.isMain = false;
            newMainPhoto.isMain = true;
            
            if (await _repo.SaveAll()) {
                return NoContent();
            }
            return BadRequest ("Could not Change");

        }
 [HttpDelete("{id}")]

 public async Task<IActionResult> DeletePhoto (int Userid, int id) {

            if (Userid != int.Parse (User.FindFirst(ClaimTypes.NameIdentifier).Value)) {
                return Unauthorized ();
            }
            var userPhtoMain = await _repo.GetUser(Userid);

              if (!userPhtoMain.Photos.Any(p => p.Id== id)) {
                return Unauthorized ();
            }
            var PhotoDelete = await _repo.GetPhoto(id);
            if (PhotoDelete.isMain)
                return BadRequest ("Can not delete the main photo");
            var photoDelete = new DeletionParams(PhotoDelete.PhotoId) ;
           var resulit =_cloudinary.Destroy(photoDelete);
           if(resulit.Result=="ok") {
               _repo.Delete(PhotoDelete);
               if (await _repo.SaveAll() ){
                 return Ok();
                }}
           if(resulit.Result!="ok") {
            _repo.Delete(PhotoDelete);
               if (await _repo.SaveAll() ){
                 return Ok();
                 }
              
            }
          return BadRequest("Could not delete the photo");
 }

    }
}