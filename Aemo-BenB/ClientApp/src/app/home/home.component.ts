import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TextMatch } from '../text-match/textmatch';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  matches;
  multipleMatchesDefault = true;
  caseSensitiveDefault = false;

  model = new TextMatch('', '', this.multipleMatchesDefault, this.caseSensitiveDefault);
  submitted = false;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) { }

  public textMatch() {
    this.submitted = true;
    const controller = `textmatch`;
    const params = `text=${this.model.text}&subtext=${this.model.subtext}&multiplematches=${this.model.multiplematches}&casesensitive=${this.model.casesensitive}`;
    const url = `${this.baseUrl}${controller}?${params}`;
    this.http.get<[]>(url).subscribe(result => {
      this.matches = result;
    }, error => console.error(error));
  }

}
