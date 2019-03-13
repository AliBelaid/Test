import { AlertifyjsService } from './../_Services/alertifyjs.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_Services/auth.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { User } from '../_models/User';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() CancelFromRegister = new EventEmitter();
  user: User;
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(private auth: AuthService, private rout: Router, private alert: AlertifyjsService, private fb: FormBuilder) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-red'
    },
      this.createFormBulider();
  }
  createFormBulider() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      gender: ['male', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: [null, Validators.required],
      password: ['', [Validators.required, Validators.maxLength(8), Validators.minLength(4)]],
      confirmPassword: ['', Validators.required],
    }, { validator: this.getChackPassword });

  }
  getChackPassword(g: FormGroup) {
    return g.get('confirmPassword').value === g.get('password').value ? null : { 'miss': true };
  }

  Cancel() {
    this.CancelFromRegister.emit(false);
    this.alert.warning('canceled');
  }

  RegisterIn() {

    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.auth.Register(this.user).subscribe(() => {
        this.alert.success('Create new user');
      }, error => {
        this.alert.success(error);
      }, () => {
        this.auth.Login(this.user).subscribe(() => {
          this.rout.navigate(['/Member']);
        });
      });
    }

    //  this.auth.Register(this.model).subscribe(() => {
    // this.alert.success('successed');
    // },  error => {
    //   this.alert.error(error);

    // });
    console.log(this.registerForm);
  }
}
