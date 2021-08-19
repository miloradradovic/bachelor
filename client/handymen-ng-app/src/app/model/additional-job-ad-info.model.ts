export class AdditionalJobAdInfoModel {
  public id: number;
  public urgent: boolean;
  public priceMax: number;

  constructor(id: number, urgent: boolean, priceMax: number) {
    this.id = id;
    if (!this.urgent) {
      this.urgent = false;
    } else {
      this.urgent = urgent;
    }

    if (!this.priceMax) {
      this.priceMax = 0;
    } else {
      this.priceMax = priceMax;
    }
  }
}
