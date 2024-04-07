import { Button } from 'flowbite-react';
import React, { useState } from 'react';
import Modal from 'react-modal';

interface ModalProps {
    isOpen: boolean;
    onClose: () => void;
    title?: string;
    content: React.ReactNode;
    actionButtonLabel?: string;
    onActionButtonClick?: () => void;
    cancelButtonLabel?: string;
    onCancelButtonClick?: () => void;
    loading?: boolean;
}

const customStyles = {
    overlay: {
        backgroundColor: 'rgba(0, 0, 0, 0.5)', // darker semi transparent dark background
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
    },
    content: {
        border: 'none', // no border
        borderRadius: '0.5rem', // rounded corners
        boxShadow: '0 0 20px rgba(0, 0, 0, 0.4)', // cooler shadow effect
        maxWidth: '500px', // maximum width of the modal
        margin: 'auto', // center the modal horizontally
    },
};


const CustomModal: React.FC<ModalProps> = ({
    isOpen,
    onClose,
    title,
    content,
    actionButtonLabel = 'Confirm',
    onActionButtonClick,
    cancelButtonLabel = 'Cancel',
    onCancelButtonClick,
    loading = false
}) => {
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
                <div className="modal-body mb-4">
                    {content && <p className="text-lg text-third">{content}</p>}
                </div>
                <div className="modal-footer flex justify-end flex-row space-x-7">
                    {onCancelButtonClick && (
                        <button className="modal-cancel btn btn-secondary mr-2 bg-secondary text-primary font-semibold px-4 py-2 rounded-lg hover:bg-third focus:outline-none" onClick={onCancelButtonClick}>
                            {cancelButtonLabel}
                        </button>
                    )}
                    {onActionButtonClick && (
                        <Button
                            color='failure'
                            isProcessing={loading}
                            className="modal-action"
                            onClick={onActionButtonClick}
                        >
                            {actionButtonLabel}
                        </Button>
                    )}
                </div>
            </div>
        </Modal>
    );
};

export default CustomModal;
