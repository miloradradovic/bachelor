import {LocationModel} from './location.model';

export class ProfileDataModel {
  public id: number;
  public firstName: string;
  public lastName: string;
  public email: string;
  public location: LocationModel;
  public trades: string[];

  constructor(id: number, firstName: string, lastName: string, email: string,
              location: LocationModel, trades: string[]) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.location = location;
    this.trades = trades;
  }
}
