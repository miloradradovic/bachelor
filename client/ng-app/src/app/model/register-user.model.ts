import {LocationModel} from './location.model';

export class RegisterUserModel {
    public firstName: string;
    public lastName: string;
    public email: string;
    public password: string;
    public verified: boolean;
    public location: LocationModel

    constructor(firstName: string, lastName: string, email: string, password: string, verified: boolean, location: LocationModel) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        this.verified = verified;
        this.location = location;
    }
}
