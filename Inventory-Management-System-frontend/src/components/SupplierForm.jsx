import { useState } from 'react';
import { createSupplier } from '../services/supplierService';

const SupplierForm = ({ onSupplierAdded }) => {
    const [name, setName] = useState('');
    const [phone, setPhone] = useState('');
    const [email, setEmail] = useState('');
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!name.trim() || !phone.trim() || !email.trim()) {
            setError("All fields are required.");
            return;
        }

        setIsSubmitting(true);
        setError(null);

        try {
            await createSupplier({ name, phone, email });
            setName('');
            setPhone('');
            setEmail('');
            if (onSupplierAdded) onSupplierAdded();
        } catch (err) {
            console.error(err);
            setError("Failed to create supplier.");
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <div style={{ border: '1px solid #ccc', padding: '15px', marginBottom: '20px', borderRadius: '5px' }}>
            <h3>Add New Supplier</h3>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleSubmit} style={{ display: 'flex', gap: '10px', flexWrap: 'wrap' }}>
                <input type="text" placeholder="Name" value={name} onChange={(e) => setName(e.target.value)} disabled={isSubmitting} style={{ padding: '8px' }} />
                <input type="text" placeholder="Phone" value={phone} onChange={(e) => setPhone(e.target.value)} disabled={isSubmitting} style={{ padding: '8px' }} />
                <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} disabled={isSubmitting} style={{ padding: '8px' }} />
                <button type="submit" disabled={isSubmitting} style={{ padding: '8px 15px', cursor: 'pointer' }}>
                    {isSubmitting ? 'Saving...' : 'Save Supplier'}
                </button>
            </form>
        </div>
    );
};

export default SupplierForm;