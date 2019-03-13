import { AlertifyjsService } from './../../_Services/alertifyjs.service';
import { AuthService } from './../../_Services/auth.service';
import { Message } from './../../_models/Message';
import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/_Services/user.service';
import { pipe } from '@angular/core/src/render3/pipe';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @Input() recipientId: number;
  messages: Message[];
  messageSend: any = {};
  constructor(private auth: AuthService, private userSerives: UserService, private alertify: AlertifyjsService) { }

  ngOnInit() {
    this.LoadThreadMessages();
  }
  LoadThreadMessages() {
    const userCurreny = +this.auth.TokenDecoder.nameid;
    this.userSerives.getMessageThread(this.auth.TokenDecoder.nameid, this.recipientId)
      .pipe(tap(
        mesaages => {
          for (let i = 0; i < mesaages.length; i++) {
            if (mesaages[i].isRead === false && mesaages[i].recipientId === userCurreny) {
              this.userSerives.markAsRead(userCurreny, mesaages[i].id);
              console.log(userCurreny);
                        }
          }
        })).subscribe(
          (message) => {
            this.messages = message;
          },
          error => {
            this.alertify.error(error);
          }
        );
  }
  SendMessage() {
    this.messageSend.recipientId = this.recipientId;
    console.log(this.messageSend);

    this.userSerives.sendMessage(this.auth.TokenDecoder.nameid, this.messageSend).subscribe((message: Message) => {
      this.messages.unshift(message);
      this.messageSend.context = '';
    },
      error => {
        this.alertify.error(error);
      });
  }
}
