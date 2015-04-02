import {Component, Template} from 'angular2/angular2';
import {If} from 'angular2/angular2';
import {Zippy} from 'zippy';

@Component({
    selector: 'hello'  //TODO: default to camel-cased class name if not provided?
})
@Template({
    inline: `<zippy title_attr='My title!'>Content from Hello template!!!</zippy>`,
    directives: [Zippy]
})
export class Hello {
    
}
