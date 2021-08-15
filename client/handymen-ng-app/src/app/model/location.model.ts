export class LocationModel {
  public latitude: number;
  public longitude: number;
  public name: string;
  public radius: number;

  constructor(latitude: number, longitude: number, address: string, radius: number) {
    this.latitude = latitude;
    this.longitude = longitude;
    this.name = address;
    this.radius = radius;
  }
}
