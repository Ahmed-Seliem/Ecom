import { Component, Input, Output ,EventEmitter} from '@angular/core';

@Component({
  selector: 'app-pagination',
  standalone: false,
  templateUrl: './pagination.html',
  styleUrl: './pagination.scss'
})
export class Pagination {
  @Input()pageSize:number
  @Input()totalCount:number
  @Output()PageChanged= new EventEmitter();
  OnChangePage(event:any)
  {
    this.PageChanged.emit(event)
  }
}
