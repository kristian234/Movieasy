import { Button } from 'flowbite-react';
import Modal from 'react-modal';

const customStyles = {
    overlay: {
        backgroundColor: 'rgba(0, 0, 0, 0.5)', // semi-transparent dark background
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
    },
    content: {
        border: 'none', // no border
        borderRadius: '0.5rem', // rounded corners
        boxShadow: '0 0 20px rgba(0, 0, 0, 0.2)', // cooler shadow effect
        maxWidth: '500px', // maximum width of the modal
        margin: 'auto', // center the modal horizontally
    },
};

interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
    title?: string;
    cancelButtonLabel?: string;
    onCancelButtonClick?: () => void;
}

export default function ActorsModal({isOpen, onClose, title, cancelButtonLabel, onCancelButtonClick} : ModalProps) {
    return (
        <Modal
            isOpen={isOpen}
            onRequestClose={onClose}
            className="modal-wrapper"
            style={customStyles}
        >
            <div className="modal-content bg-header rounded-lg shadow-3xl p-6">
                <div className="modal-header mb-2">
                    {title && <h1 className='text-2xl text-secondary font-semibold'>{title}</h1>}
                </div>
               

                <div className="modal-footer flex justify-end flex-row space-x-7">
                    {onCancelButtonClick && (
                        <button className="modal-cancel btn btn-secondary mr-2 bg-secondary text-primary font-semibold px-4 py-2 rounded-lg hover:bg-third focus:outline-none" onClick={onCancelButtonClick}>
                            {cancelButtonLabel}
                        </button>
                    )}
                </div>
            </div>
        </Modal>
    )
}