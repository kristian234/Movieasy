'use client'

import { ReactNode, useEffect } from 'react';
import Modal from 'react-modal';

interface Props {
    children: ReactNode;
}

const ModalWrapper = ({ children }: Props) => {
    useEffect(() => {
        Modal.setAppElement('body');
    }, []);

    return children;
};

export default ModalWrapper;
