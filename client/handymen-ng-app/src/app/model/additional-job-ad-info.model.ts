export class AdditionalJobAdInfoModel {
  public id: number;
  public urgent: boolean;
  public priceMax: number;

  constructor(id: number, urgent: boolean, priceMax: number) {
    this.id = id;
    this.urgent = urgent;
    this.priceMax = priceMax;
  }
}
