export class LoggedInModel {
  public id: number
  public email: string;
  public role: string;

  constructor(id: number, email: string, role: string) {
    this.email = email;
    this.id = id;
    this.role = role;
  }
}
