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

    this.api.obterDepartamentos().subscribe(deps => {
      this.departamentos = deps;
      
      if (this.produtoParaEditar) {
        this.carregarDadosProduto();
      }
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['produtoParaEditar']) {
      if (this.produtoParaEditar) {
        if (this.departamentos.length > 0) {
          this.carregarDadosProduto();
        }
      } else {
        this.resetarFormulario();
      }
    }
  }

  private carregarDadosProduto() {
    if (!this.form || !this.produtoParaEditar) {
      return;
    }
 
    this.titulo = 'Editar Produto';
    this.produtoId = this.produtoParaEditar.Id;

    const departamentoId = this.buscarIdDepartamento(this.produtoParaEditar.Departamento);
    
    const dadosFormulario = {
      codigo: this.produtoParaEditar.Codigo,
      descricao: this.produtoParaEditar.Descricao,
      departamentoId: departamentoId,
      preco: this.produtoParaEditar.Preco
    };

    this.form.patchValue(dadosFormulario);
  }

  private buscarIdDepartamento(valorDepartamento: string): string {

    const porId = this.departamentos.find(dep => dep.id === valorDepartamento);
    if (porId) {
      return valorDepartamento;
    }
    
    const porNome = this.departamentos.find(dep => 
      dep.descricao === valorDepartamento ||
      dep.descricao?.toLowerCase() === valorDepartamento?.toLowerCase()
    );
    
    const idEncontrado = porNome ? porNome.id : '';
    
    return idEncontrado;
  }

  private resetarFormulario() {
    if (!this.form) {
      return;
    }

    this.titulo = 'Adicionar Produto';
    this.produtoId = '';
    this.form.reset();
    this.form.patchValue({ preco: 0 });
  }

  salvar() {
    if (this.form.invalid) {
      return;
    }

const produtoPayload = {
  codigo: this.form.value.codigo,
  status: true,
  descricao: this.form.value.descricao,
  departamentoId: this.form.value.departamentoId,
  preco: this.form.value.preco
};

//console.log('Payload:', produtoPayload);

const operacao = this.produtoParaEditar
  ? this.api.atualizarProduto(this.produtoParaEditar.Id, produtoPayload)
  : this.api.adicionarProduto(produtoPayload);

    operacao.subscribe({
      next: () => {
        this.salvo.emit();
        this.fechar.emit();
      },
      error: (err) => {
        console.error('Erro ao salvar produto:', err);
      }
    });
  }

  cancelar() {
    this.fechar.emit();
  }
}