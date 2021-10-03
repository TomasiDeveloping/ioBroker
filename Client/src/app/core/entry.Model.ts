export interface EntryModel {
  id: number;
  dataPointName: string;
  timeStamp: Date;
  confirmation: boolean;
  adapter: number;
  adapterName: string;
  quality: number;
}
