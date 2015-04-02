import {Component, Template} from 'angular2/angular2';

@Component({
  selector: 'name-tag',
  bind: {
    'name': 'name'
  }
})
@Template({
  url: "name_tag.html"
})
export class NameTag {

}