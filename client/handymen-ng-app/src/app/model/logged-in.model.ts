export class LoggedInModel {
  public id: number
  public email: string;
  public role: string;
  public token: string;

  constructor(id: number, email: string, role: string, token: string) {
    this.email = email;
    this.id = id;
    this.role = role;
    this.token = token;
  }
}
