export class SearchParams {
  public firstName: string;
  public lastName: string;
  public trades: string[];
  public avgRatingFrom: number;
  public avgRatingTo: number;

  constructor(firstName: string, lastName: string, trades: string[], avgRatingFrom: number, avgRatingTo: number) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.trades = trades;
    this.avgRatingFrom = avgRatingFrom;
    this.avgRatingTo = avgRatingTo;
  }
}
