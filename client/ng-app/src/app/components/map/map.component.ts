import {Component, Input, OnInit, Output, ViewChild, EventEmitter} from '@angular/core';
import {AgmCircle, AgmMap, LatLngLiteral} from '@agm/core';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  @ViewChild(AgmMap) agmMap: AgmMap;
  @ViewChild(AgmCircle) agmCircle: AgmCircle

  @Input() latitude = 10.0;
  @Input() longitude = 10.0;
  @Input() draggable = true;
  @Input() showCircle = true;
  @Output() DragEnd = new EventEmitter<number>();
  zoom = 10;
  location = '';
  radius = 5000;


  constructor() { }

  ngOnInit(): void {
  }


  dragEnd(coords: LatLngLiteral) {
    this.latitude = coords.lat;
    this.longitude = coords.lng;
    // @ts-ignore
    const opencage = require('opencage-api-client');
    opencage.geocode({q: '' + coords.lat + ', ' + coords.lng, key: 'df145e8c933d49e399e5d6703a1e88b1'}).then(data => {
      if (data.status.code === 200) {
        this.location = data.results[0].formatted;
        console.log(this.location);
        // this.DragEnd.emit(new MapDataModel(coords.lat, coords.lng, data.results[0].formatted));
      }
    }).catch(error => {
    });
    console.log(coords);
    console.log(this.radius);
  }

  radiusChanged($event: number) {
    this.radius = $event;
  }
}
