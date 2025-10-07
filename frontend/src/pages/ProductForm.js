import React, {useEffect, useState} from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../api';

export default function ProductForm(){
  const { id } = useParams();
  const isEdit = Boolean(id);
  const navigate = useNavigate();
  const [name,setName]=useState('');
  const [description,setDescription]=useState('');
  const [price,setPrice]=useState('');
  const [categoryId,setCategoryId]=useState('');
  const [categories,setCategories]=useState([]);
  const [loading,setLoading]=useState(false);

  useEffect(()=>{ fetchCategories(); if(isEdit) fetchProduct(); },[]);

  async function fetchCategories(){ const res=await api.get('/categories'); setCategories(res.data); }
  async function fetchProduct(){ setLoading(true); const res=await api.get(`/products/${id}`); const p=res.data; setName(p.name); setDescription(p.description||''); setPrice(p.price); setCategoryId(p.categoryId); setLoading(false); }

  async function onSubmit(e){ e.preventDefault(); const payload={ name, description, price: parseFloat(price), categoryId: parseInt(categoryId) }; try{ if(isEdit) await api.put(`/products/${id}`, payload); else await api.post('/products', payload); navigate('/products'); }catch(e){ alert('Save failed'); } }

  return (<div><h2>{isEdit?'Edit':'Add'} Product</h2>{loading? <div>Loading...</div> : (<form onSubmit={onSubmit}><div className="mb-3"><label className="form-label">Name</label><input className="form-control" value={name} onChange={e=>setName(e.target.value)} required maxLength={200} /></div><div className="mb-3"><label className="form-label">Description</label><textarea className="form-control" value={description} onChange={e=>setDescription(e.target.value)} /></div><div className="mb-3"><label className="form-label">Price</label><input type="number" step="0.01" className="form-control" value={price} onChange={e=>setPrice(e.target.value)} required /></div><div className="mb-3"><label className="form-label">Category</label><select className="form-select" value={categoryId} onChange={e=>setCategoryId(e.target.value)} required><option value=''>Select</option>{categories.map(c=><option key={c.id} value={c.id}>{c.name}</option>)}</select></div><button className="btn btn-primary" type="submit">Save</button></form>)}</div>);
}
