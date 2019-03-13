namespace DatingApp.api.Helpers
{
    public class MessageParems
    {        public int Maxsize =50;
      
       public int PageNumber { get; set; } =1;
         private int pageSize =10;
         public int Userid { get; set; }
         public string MessageContainer { get; set; } ="UnRead";
          public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value>Maxsize) ? Maxsize:value;}
        }
    }
}