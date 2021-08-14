import {LocationModel} from './location.model';

export class RegisterHandymanModel {
    public firstName: string;
    public lastName: string;
    public email: string;
    public password: string;
    public verified: boolean;
    public location: LocationModel
    public trades: string[]

    constructor(firstName: string, lastName: string, email: string, password: string, verified: boolean,
                location: LocationModel, trades: string[]) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        this.verified = verified;
        this.location = location;
        this.trades = trades;
    }
}
