export class LogIn {
    private username: string;
    private password: string;

    constructor(email: string, password: string) {
      this.username = email;
      this.password = password;
    }
  }

  export enum UserRole {
    UNAUTHORIZED,
    USER,
    ADMIN
  }

export class LogInModel {
    constructor(
      private username: string,
      private accessToken: string,
      private id: number,
      private role: string
    ) {
    }

    getRole(): UserRole {
      return this.role === 'ROLE_USER' ? UserRole.USER : UserRole.UNAUTHORIZED;
    }
  }

export interface RegisterModel {
    firstName: String,
    lastName: String,
    username: String,
    password: String,
    age: number
}
