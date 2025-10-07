import React, {useEffect, useState} from 'react';
import api from '../api';

export default function CategoryList(){
  const [cats,setCats]=useState([]);
  useEffect(()=>{ fetch(); },[]);
  async function fetch(){ const res = await api.get('/categories'); setCats(res.data); }
  return (<div><h2>Categories</h2><ul className="list-group">{cats.map(c=><li key={c.id} className="list-group-item">{c.name}</li>)}</ul></div>);
}
