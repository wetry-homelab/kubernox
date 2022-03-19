export interface CoreResponse<T> {
    success: boolean;
    error: any;
    errorMessage: any;
    additionalData: any;
    data: T;
}