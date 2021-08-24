export class HandymanVerificationModel {
  public id: number;
  public verify: boolean;
  public message: string;

  constructor(id: number, verify: boolean, message: string) {
    this.id = id;
    this.verify = verify;
    this.message = message;
  }
}
