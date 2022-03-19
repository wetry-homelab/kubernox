export interface AuthenticationResponse {
    username: string;
    id: string;
    token: string;
    refreshToken: string;
    passwordToken: string;
    passwordExpire: boolean;
}