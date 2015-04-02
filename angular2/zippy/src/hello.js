import {Component, Template} from 'angular2/angular2';
import {If} from 'angular2/angular2';
import {Zippy} from 'zippy';
import {NameTag} from 'name_tag'

@Component({
    selector: 'hello'  //TODO: default to camel-cased class name if not provided?
})
@Template({
    url: 'hello.html',
    directives: [Zippy, NameTag]
})
export class Hello {
    constructor() {
      this.inputName = "Limo";
    }

    updateName(newName) {
      this.inputName = newName;
    }
}
