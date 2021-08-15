import {Component, Input, OnInit, Output, ViewChild, EventEmitter} from '@angular/core';
import {AgmCircle, AgmMap, LatLngLiteral} from '@agm/core';
import {LocationModel} from '../../model/location.model';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {

  @ViewChild(AgmMap) agmMap: AgmMap;
  @ViewChild(AgmCircle) agmCircle: AgmCircle

  latitude = 45.259452102126545;
  longitude = 19.848492145538334;
  @Input() draggable = true;
  @Input() showCircle = true;
  @Output() DragEnd = new EventEmitter<LocationModel>();
  @Output() RadiusChanged = new EventEmitter<number>();
  zoom = 15;
  location = '';
  radius = 500;


  constructor() { }

  ngOnInit(): void {
  }


  dragEnd(coords: LatLngLiteral) {
    this.latitude = coords.lat;
    this.longitude = coords.lng;
    console.log('DRAG END RADIUS ', this.radius);
    // @ts-ignore
    const opencage = require('opencage-api-client');
    opencage.geocode({q: '' + coords.lat + ', ' + coords.lng, key: 'df145e8c933d49e399e5d6703a1e88b1'}).then(data => {
      if (data.status.code === 200) {
        this.location = data.results[0].formatted;
        console.log(this.location);
        this.DragEnd.emit(new LocationModel(coords.lat, coords.lng, data.results[0].formatted, this.radius));
      }
    }).catch(error => {
    });
  }

  radiusChanged($event: number) {
    this.radius = $event;
    this.RadiusChanged.emit($event);
  }
}
