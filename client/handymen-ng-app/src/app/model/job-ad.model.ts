import {LocationModel} from './location.model';
import {AdditionalJobAdInfoModel} from './additional-job-ad-info.model';

export class JobAdModel {
  public id: number;
  public title: string;
  public description: string;
  public address: LocationModel;
  public additionalJobAdInfo: AdditionalJobAdInfoModel;
  public dateWhen: Date;
  public trades: string[];
  public pictures: string[];

  constructor(id: number, title: string, description: string, address: LocationModel, additionalJobAdInfo: AdditionalJobAdInfoModel,
              dateWhen: Date, trades: string[], pictures: string[]) {
    this.id = id;
    this.title = title;
    this.description = description;
    this.address = address;
    this.additionalJobAdInfo = additionalJobAdInfo;
    this.dateWhen = dateWhen;
    this.trades = trades;
    this.pictures = pictures;
  }
}
