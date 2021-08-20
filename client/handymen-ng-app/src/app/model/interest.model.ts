export class InterestModel {
  public id: number
  public jobAdId: number;
  public daysEstimated: number;
  public priceEstimated: number;

  constructor(id: number, jobAdId: number, daysEstimated: number, priceEstimated: number) {
    this.id = id;
    this.jobAdId = jobAdId;
    this.daysEstimated = daysEstimated;
    this.priceEstimated = priceEstimated;
  }
}
