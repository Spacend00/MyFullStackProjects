import { Routes } from '@angular/router';
import { MainPage } from './main-page/main-page';
import { ExamplesPage } from './examples-page/examples-page';

export const routes: Routes = [
    { path: '', component: MainPage},
    { path: 'examples', component: ExamplesPage}
];
