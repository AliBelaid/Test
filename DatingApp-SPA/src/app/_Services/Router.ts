import { MemberEditComponent } from './../members/member-edit/member-edit.component';
import { MemberDetailResolve } from './../_resolves/member-detail.resolve';
import { MemberDetailComponent } from './../members/member-detail/member-detail.component';
import { ListComponent } from './../list/list.component';
import { MemberListComponent } from './../members/member-list/member-list.component';
import { MessageComponent } from './../message/message.component';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../Home/Home.component';
import { AuthGuard } from '../_graud/auth.guard';
import { MemeberListResolve } from './../_resolves/member-list';
import { MemberEditResolve } from './../_resolves/member-edit';
import { PreventUnsavdChange } from '../_graud/prevent-unsavd-change.guard';
import { ListResolve } from '../_resolves/list.resolve';
import { MessagesResolve } from './../_resolves/messages_resolves';



export const app_route: Routes = [
  { path: '', component: HomeComponent },
  {
    path: ''
    , runGuardsAndResolvers: 'always', canActivate: [AuthGuard],
    children: [{ path: 'Message', component: MessageComponent, resolve: { messages: MessagesResolve } },
    { path: 'List', component: ListComponent, resolve: { user: ListResolve } },
    { path: 'Member', component: MemberListComponent, resolve: { user: MemeberListResolve } },
    {
      path: 'Member/:id', component: MemberDetailComponent,
      resolve: { user: MemberDetailResolve }
    },
    {
      path: 'member/edit', component: MemberEditComponent
      , resolve: { user: MemberEditResolve }, canDeactivate: [PreventUnsavdChange]
    }
    ]
  }, { path: '**', component: HomeComponent }
];

