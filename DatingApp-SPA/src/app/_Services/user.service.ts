import { Message } from './../_models/Message';
import { Pagination } from 'src/app/_models/Pagination';
import { map } from 'rxjs/operators';
import { PaginationReuslt } from './../_models/Pagination';
import { environment } from './../../environments/environment';
import { User } from './../_models/User';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, observable } from 'rxjs';

//  const httpOption = {
//  headers: new HttpHeaders({
//   'Authorization' : 'Bearer ' + localStorage.getItem('token')
// })
// };
@Injectable({
  providedIn: 'root'
})

export class UserService {

  UrlBas = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(pageNumber?, pageSize?, userParem?, likeParem?): Observable<PaginationReuslt<User[]>> {

    const paginationSend: PaginationReuslt<User[]> = new PaginationReuslt<User[]>();

    let params = new HttpParams();
    if (pageNumber != null && pageSize != null) {
      params = params.append('PageNumber', pageNumber);
      params = params.append('PageSize', pageSize);
    }
    if (userParem != null) {
      params = params.append('MaxAge', userParem.maxAge);
      params = params.append('MinAge', userParem.minAge);
      params = params.append('Gender', userParem.gender);
      params = params.append('OrderBy', userParem.orderBy);

    }
    if (likeParem === 'likers') {
      params = params.append('Likers', 'true');
    }
    if (likeParem === 'likees') {
      params = params.append('Likees', 'true');
    }
    return this.http.get<User[]>(this.UrlBas + 'User', { observe: 'response', params })
      .pipe(map(response => {
        paginationSend.result = response.body;
        if (response.headers.get('pagination') != null) {
          paginationSend.pagination = JSON.parse(response.headers.get('pagination'));
          console.log(paginationSend.result);

        }
        return paginationSend;
      }));

  }
  getUser(id): Observable<User> {
    return this.http.get<User>(this.UrlBas + 'User/' + id);
  }
  updateUser(id: number, user: User) {
    return this.http.put(this.UrlBas + 'User/' + id, user);
  }

  updateMainPhto(userId: number, idPhoto: number) {
    return this.http.post(this.UrlBas + 'user/' + userId + '/photo/' + idPhoto + '/setMain', {});
  }
  deltePhoto(userId: number, idPhoto: number) {
    return this.http.delete(this.UrlBas + 'user/' + userId + '/photo/' + idPhoto);
  }
  addLike(userId: number, recipinetId: number) {
    return this.http.post(this.UrlBas + 'user/' + userId + '/like/' + recipinetId, {});
  }
  getMessage(Id: number, page?, itemPerPage?, messageContainer?) {
    const paginatiedReuslt: PaginationReuslt<Message[]> = new PaginationReuslt<Message[]>();
    let params = new HttpParams();
    params = params.append('MessageContainer', messageContainer);
    if (page != null && itemPerPage != null) {
      params = params.append('PageNumber', page);
      params = params.append('PageSize', itemPerPage);
    }
    return this.http.get<Message[]>(this.UrlBas + 'User/' + Id + '/Message',
      { observe: 'response', params })
      .pipe(
        map(response => {
          paginatiedReuslt.result = response.body;
          if (response.headers.get('pagination') != null) {
            paginatiedReuslt.pagination = JSON.parse(response.headers.get('pagination'));
          }
          return paginatiedReuslt;
        }));
  }
  getMessageThread(id: number, recipientId: number) {
    return this.http.get<Message[]>(this.UrlBas + 'User/' + id + '/Message/Thread/' + recipientId);
  }
  sendMessage(id: number, message: Message) {
    return this.http.post(this.UrlBas + 'User/' + id + '/Message', message);
  }
  deleteMessage(id: number, recipientId: number) {
    return this.http.delete(this.UrlBas + 'User/' + id + '/Message/' + recipientId);
  }
  markAsRead(userid: number, id: number) {
    return this.http.post(this.UrlBas + 'User/' + userid + '/Message/' + id + '/Read', {}).subscribe();
  }
}
