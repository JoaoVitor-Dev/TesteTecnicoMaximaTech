import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { ApiService } from '../../services/api.service';
import { Produto } from '../../interfaces/produto.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule], 
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  produtos: Produto[] = [];

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
    console.log('Clique em adicionar produto');
  }

  editar(produto: Produto) {
    console.log('Editar produto:', produto);
  }

  inativar(produto: Produto) {
    if (!confirm(`Deseja realmente inativar o produto "${produto.descricao}"?`)) return;

    this.apiService.inativarProduto(produto.id).subscribe({
      next: () => this.carregarProdutos(),
      error: (error) => console.error('Erro ao inativar produto:', error)
    });
  }
}
