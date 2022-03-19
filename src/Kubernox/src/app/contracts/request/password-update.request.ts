export interface PasswordUpdateRequest {
    id: string;
    username: string;
    passwordToken: string;
    password: string;
    repeatedPassword: string;
}