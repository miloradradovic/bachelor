export class RatingModel {
  public id: number;
  public rate: number;
  public description: string;
  public jobId: number;

  constructor(id: number, rate: number, description: string, jobId: number) {
    this.id = id;
    this.rate = rate;
    this.description = description;
    this.jobId = jobId;
  }
}
