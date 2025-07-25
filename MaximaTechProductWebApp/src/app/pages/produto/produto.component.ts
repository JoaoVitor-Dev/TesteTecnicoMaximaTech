import { Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { Departamento } from '../../interfaces/departamento.interface';
import { Produto } from '../../interfaces/produto.interface';

@Component({
  selector: 'app-produto',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.css']
})
export class ProdutoComponent implements OnInit, OnChanges {

  @Input() produtoParaEditar?: Produto | null;
  @Output() fechar = new EventEmitter<void>();
  @Output() salvo = new EventEmitter<void>();

  form!: FormGroup;
  departamentos: Departamento[] = [];
  produtoId = '';
  titulo = 'Adicionar Produto';

  constructor(
    private fb: FormBuilder,
    private api: ApiService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.form = this.fb.group({
      codigo: ['', Validators.required],
      descricao: ['', Validators.required],
      departamentoId: ['', Validators.required],
      preco: [0, [Validators.required, Validators.min(0.01)]]
    });

    this.api.obterDepartamentos().subscribe(deps => this.departamentos = deps);

    if (this.produtoParaEditar) {
      this.titulo = 'Editar Produto';
      this.form.patchValue(this.produtoParaEditar);
      this.produtoId = this.produtoParaEditar.id;
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['produtoParaEditar'] && this.form) {
      if (this.produtoParaEditar) {
        this.titulo = 'Editar Produto';
        this.form.patchValue(this.produtoParaEditar);
        this.produtoId = this.produtoParaEditar.id;
      } else {
        this.titulo = 'Adicionar Produto';
        this.form.reset();
        this.form.patchValue({ preco: 0 });
      }
    }
  }

 salvar() {
  if (this.form.invalid) return;

  const produto: Produto = {
    ...this.form.value,
    status: true 
  };

  const operacao = this.produtoParaEditar
    ? this.api.atualizarProduto(this.produtoParaEditar.id, produto)
    : this.api.adicionarProduto(produto);

  operacao.subscribe({
    next: () => {
      this.salvo.emit();
      this.fechar.emit();
    },
    error: (err) => console.error('Erro ao salvar produto:', err)
  });
}


  cancelar() {
     this.fechar.emit();
  }

}
