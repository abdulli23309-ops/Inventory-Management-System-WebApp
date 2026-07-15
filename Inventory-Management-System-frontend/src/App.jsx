import { useState } from 'react';
import CategoryList from './components/CategoryList';
import SupplierList from './components/SupplierList';
import ProductList from './components/ProductList';
import PurchaseManager from './components/PurchaseManager';
import SaleManager from './components/SaleManager';
import Reports from './components/Reports';

function App() {
  const [activeTab, setActiveTab] = useState('categories');

  const renderContent = () => {
    switch (activeTab) {
      case 'categories':
        return <CategoryList />;
      case 'suppliers':
        return <SupplierList />;
      case 'products':
        return <ProductList />;
      case 'purchases':
        return <PurchaseManager />;
      case 'sales':
        return <SaleManager />;
      case 'reports':
        return <Reports />;
      default:
        return <CategoryList />;
    }
  };

  const buttonStyle = (tabName) => ({
    padding: '10px 20px',
    cursor: 'pointer',
    background: activeTab === tabName ? '#0056b3' : '#2d2d2d',
    color: activeTab === tabName ? 'white' : '#e0e0e0',
    border: '1px solid #444',
    borderRadius: '4px',
    fontWeight: 'bold',
    transition: 'all 0.2s ease'
  });

  return (
    <div style={{ padding: '30px', fontFamily: 'Segoe UI, Helvetica, sans-serif', maxWidth: '1200px', margin: '0 auto', color: '#e0e0e0' }}>
      
      {/* HEADER - Fixed Overlap */}
      <header style={{ 
        borderBottom: '3px solid #0056b3', 
        paddingBottom: '20px', 
        marginBottom: '25px',
        display: 'flex',
        flexDirection: 'column', 
        alignItems: 'center',
        gap: '10px' // This prevents the overlap!
      }}>
        <h1 style={{ color: '#4da3ff', margin: 0, textAlign: 'center', lineHeight: '1' }}>
          Inventory Management System
        </h1>
        <p style={{ margin: 0, color: '#aaa', fontSize: '1.2rem', textAlign: 'center' }}>
          Lead Developer Control Center
        </p>
      </header>

      {/* Navigation tabs - Updated for Dark Mode */}
      <nav style={{ display: 'flex', gap: '10px', marginBottom: '30px', flexWrap: 'wrap', justifyContent: 'center' }}>
        <button style={buttonStyle('categories')} onClick={() => setActiveTab('categories')}>Categories</button>
        <button style={buttonStyle('suppliers')} onClick={() => setActiveTab('suppliers')}>Suppliers</button>
        <button style={buttonStyle('products')} onClick={() => setActiveTab('products')}>Products</button>
        <button style={buttonStyle('purchases')} onClick={() => setActiveTab('purchases')}>Purchase Orders</button>
        <button style={buttonStyle('sales')} onClick={() => setActiveTab('sales')}>Sales Invoices</button>
        <button style={buttonStyle('reports')} onClick={() => setActiveTab('reports')}>Valuation Report</button>
      </nav>

      {/* Active Component Screen - Converted to Dark Mode */}
      <main style={{ 
        background: '#1e1e1e', // Sleek dark gray
        padding: '25px', 
        borderRadius: '8px', 
        boxShadow: '0 4px 15px rgba(0,0,0,0.4)',
        border: '1px solid #333'
      }}>
        {renderContent()}
      </main>
    </div>
  );
}

export default App;