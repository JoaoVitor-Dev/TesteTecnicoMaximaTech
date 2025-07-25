import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Departamento } from '../interfaces/departamento.interface';
import { Produto } from '../interfaces/produto.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

private baseUrl = 'https://localhost:5001/api'; 

constructor(private http: HttpClient) { }

obterDepartamentos():Observable<Departamento[]>{
  return this.http.get<Departamento[]>(`${this.baseUrl}/departamento`)
}

obterProdutos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(`${this.baseUrl}/produto`);
}

adicionarProduto(produto: Produto): Observable<Produto> {
    return this.http.post<Produto>(`${this.baseUrl}/produto`, produto);
}

atualizarProduto(id: string, produto: Produto): Observable<Produto> {
    return this.http.put<Produto>(`${this.baseUrl}/produto/${id}`, produto);
}

inativarProduto(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/produto/${id}`);
}

obterProduto(id: string): Observable<Produto> {
    return this.http.get<Produto>(`${this.baseUrl}/produto/${id}`);
}

}
