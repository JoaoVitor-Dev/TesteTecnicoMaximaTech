import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { ApiService } from '../../services/api.service';
import { Produto } from '../../interfaces/produto.interface';
import { ProdutoComponent } from '../produto/produto.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ProdutoComponent], 
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  produtos: Produto[] = [];

  produtoEmEdicao?: Produto | null = null;
  mostrarModal = false;

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.carregarProdutos();
  }

  carregarProdutos() {
    this.apiService.obterProdutos().subscribe({
      next: (data) => this.produtos = data,
      error: (error) => console.error('Erro ao carregar produtos:', error)
    });
  }

  adicionar() {
    this.produtoEmEdicao = undefined; 
    this.mostrarModal = true;
  }

  editar(produto: Produto) {
     this.produtoEmEdicao = produto;
     this.mostrarModal = true;
  }

  inativar(produto: Produto) {
    if (!confirm(`Deseja realmente inativar o produto "${produto.Descricao}"?`)) return;

    this.apiService.inativarProduto(produto.id).subscribe({
      next: () => this.carregarProdutos(),
      error: (error) => console.error('Erro ao inativar produto:', error)
    });
  }

  
fecharModal() {
  this.mostrarModal = false;
  this.produtoEmEdicao = null;
}
}
