import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'statusIcon'
})
export class StatusIconPipe implements PipeTransform {
  transform(value: string, ...args: unknown[]): unknown {
    switch(value.toLowerCase()){
    case 'prospect':
      return 'maybe';
      break;
     case 'customer':
      return 'yes';
      break;
    case 'not_interested':
      return 'nope';
      break;
    default:
      return 'whoKnows';
  }
}
}
