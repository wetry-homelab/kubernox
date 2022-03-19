import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationRequest, PasswordUpdateRequest } from 'src/app/contracts/request';
import { AuthenticationResponse } from 'src/app/contracts/response';
import { IdentityService } from 'src/app/services';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public authenticationResponse: AuthenticationResponse = {
    passwordExpire: false,
    passwordToken: '',
    refreshToken: '',
    token: '',
    username: '',
    id: ''
  };

  public loginForm: any = {};
  public passwordForm: any = {};

  constructor(private identityService: IdentityService, private toastr: ToastrService,
    private formBuilder: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('currentUser');

    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(64)]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(64)]]
    });

    this.passwordForm = this.formBuilder.group({
      passwordRepeated: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(64)]],
      password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(64)]]
    });
  }

  public loginUserAsync() {
    const request: AuthenticationRequest = {
      username: this.loginForm.controls.username.value,
      password: this.loginForm.controls.password.value,
    };

    this.identityService.authenticateUserAsync(request)
      .subscribe((success) => {
        this.authenticationResponse = success.data;
        this.authenticationSuccess();
      }, (error) => {
        this.toastr.error('Oups, an error occur during login process.');
      });
  }

  public updatePasswordAsync() {
    console.log("Update password");
    const request: PasswordUpdateRequest = {
      username: this.authenticationResponse.username,
      password: this.passwordForm.controls.password.value,
      id: this.authenticationResponse.id,
      passwordToken: this.authenticationResponse.passwordToken,
      repeatedPassword: this.passwordForm.controls.passwordRepeated.value,
    };

    this.identityService.updatePasswordAsync(request)
      .subscribe((success) => {
        this.authenticationResponse = success.data;
        if (success.success) {
          this.toastr.success("Password updated ! ");
        }
        this.authenticationSuccess();
      }, (error) => {
        this.toastr.error('Oups, an error occur during login process.');
      });
  }

  private authenticationSuccess() {
    if (this.authenticationResponse.passwordToken === null) {
      localStorage.setItem('access_token', this.authenticationResponse.token);
      localStorage.setItem('refresh_token', this.authenticationResponse.refreshToken);
      localStorage.setItem('currentUser', JSON.stringify({
        id: this.authenticationResponse.id,
        username: this.authenticationResponse.username
      }));
      this.router.navigate(['/']);
    }
  }
}
