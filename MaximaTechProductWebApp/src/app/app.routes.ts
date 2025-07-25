import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { ProdutoComponent } from './pages/produto/produto.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },  
  { path: 'produto', component: ProdutoComponent },
  { path: 'produto/:id', component: ProdutoComponent }, 
];
