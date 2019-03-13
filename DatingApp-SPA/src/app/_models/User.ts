import { Photo } from './Photo';
export interface User {
 id: number ;
  userName: string ;
  knownAs: string ;
  gender: string ;
  age: number ;
  photoUrl: string ;

   created: Date ;
  lastActive: Date ;
  interests?: string ;
  introduction?:  string ;
  lookingFor?: string;
  photos?: Photo[] ;
  city: string ;
  country: string ;

}
