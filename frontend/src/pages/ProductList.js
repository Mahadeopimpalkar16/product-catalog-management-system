import React, {useEffect, useState} from 'react';
import api from '../api';
import { Link } from 'react-router-dom';

export default function ProductList(){
  const [products,setProducts]=useState([]);
  const [categories,setCategories]=useState([]);
  const [loading,setLoading]=useState(false);
  const [page,setPage]=useState(1);
  const pageSize=10;
  const [categoryId,setCategoryId]=useState('');
  const [sortBy,setSortBy]=useState('');
  const [asc,setAsc]=useState(true);
  const [hasNext,setHasNext]=useState(false);

  useEffect(()=>{fetchCategories();},[]);
  useEffect(()=>{fetchProducts();},[page,categoryId,sortBy,asc]);

  async function fetchCategories(){ try{ const res=await api.get('/categories'); setCategories(res.data);}catch(e){console.error(e);} }
  async function fetchProducts(){ setLoading(true); try{ const params={page,pageSize,asc}; if(categoryId) params.categoryId=categoryId; if(sortBy) params.sortBy=sortBy; const res=await api.get('/products',{params}); setProducts(res.data); setHasNext(res.data.length===pageSize);}catch(e){console.error(e);} setLoading(false); }

  async function deleteProduct(id){ if(!window.confirm('Delete?')) return; try{ await api.delete(`/products/${id}`); fetchProducts(); }catch(e){ alert('Delete failed'); } }

  return (<div>
    <h2>Products</h2>
    <div className="row mb-3">
      <div className="col-md-3"><select className="form-select" value={categoryId} onChange={e=>{setCategoryId(e.target.value); setPage(1);}}><option value=''>All</option>{categories.map(c=><option key={c.id} value={c.id}>{c.name}</option>)}</select></div>
      <div className="col-md-3"><select className="form-select" value={sortBy} onChange={e=>setSortBy(e.target.value)}><option value=''>Sort By</option><option value='name'>Name</option><option value='price'>Price</option></select></div>
      <div className="col-md-2"><button className="btn btn-outline-secondary" onClick={()=>setAsc(a=>!a)}>{asc?'Asc':'Desc'}</button></div>
    </div>
    {loading? <div>Loading...</div> : (
      <table className="table table-striped">
        <thead><tr><th>Name</th><th>Category</th><th>Price</th><th>Created</th><th/></tr></thead>
        <tbody>{products.map(p=>(<tr key={p.id}><td>{p.name}</td><td>{p.categoryName}</td><td>{p.price}</td><td>{new Date(p.createdDate).toLocaleString()}</td><td><Link className="btn btn-sm btn-primary me-2" to={`/products/${p.id}/edit`}>Edit</Link><button className="btn btn-sm btn-danger" onClick={()=>deleteProduct(p.id)}>Delete</button></td></tr>))}</tbody>
      </table>
    )}
    <div className="d-flex justify-content-between my-3"><button className="btn btn-secondary" onClick={()=>setPage(p=>Math.max(1,p-1))} disabled={page<=1}>Prev</button><div>Page {page}</div><button className="btn btn-secondary" onClick={()=>setPage(p=>p+1)} disabled={!hasNext}>Next</button></div>
  </div>);
}
