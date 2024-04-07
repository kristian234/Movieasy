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
            className="modal bg-secondary rounded-lg shadow-3xl p-6"
            overlayClassName="overlay fixed top-0 left-0 right-0 bottom-0 bg-black bg-opacity-75 z-50 flex justify-center items-center"
        >
            <div className="flex-col flex text-center bg-third rounded-lg">
                <div>
                    {title && <h1 className='text-2xl font-semibold mb-2'>{title}</h1>}
                </div>
                <p className="text-lg font-semibold mb-5">{content && <p>{content}</p>}</p>
                <div className="flex justify-center space-x-4">
                    <div className="modal-actions flex-row flex space-x-5 mb-5">
                        {onCancelButtonClick && (
                            <button className="modal-cancel text-gray-600 border border-gray-400 px-4 py-2 rounded-md hover:bg-gray-100 focus:outline-none" onClick={onCancelButtonClick}>
                                {cancelButtonLabel}
                            </button>
                        )}
                        {onActionButtonClick && (
                            <Button color='failure' isProcessing={loading} className="modal-action text-white focus:outline-none" onClick={onActionButtonClick}>
                                {actionButtonLabel}
                            </Button>
                        )}

                    </div>
                </div>
            </div>

        </Modal>
    );
};

export default CustomModal;
