'use client'

import { useState, useEffect } from 'react';
import { Sidebar, Menu, MenuItem } from 'react-pro-sidebar';
import { IoMenu, IoClose } from "react-icons/io5";
import { RiMovieLine } from "react-icons/ri";
import { MdOutlineMovie } from "react-icons/md";
import { LiaTheaterMasksSolid } from "react-icons/lia";
import { MdOutlineDashboard } from "react-icons/md";

import Link from 'next/link';
import { usePathname } from 'next/navigation';
import MenuItemWithState from './menu-item';

export default function AdminHeader() {
    const [isMobile, setIsMobile] = useState(false);
    const [activeItem, setActiveItem] = useState('');
    const path = usePathname();

    useEffect(() => {
        const handleResize = () => {
            setIsMobile(window.innerWidth < 400);
        };

        window.addEventListener('resize', handleResize);
        handleResize(); // Call the function once to set the initial state

        return () => {
            window.removeEventListener('resize', handleResize);
        };
    }, []);

    useEffect(() => {
        setActiveItem(path);
    }, [path]);

    return (
        <div style={{ display: "flex", height: "100vh" }}>
            <Sidebar collapsed={isMobile} backgroundColor='' className='bg-header'>
                <div className='flex justify-center mt-2 text-third'>
                    <button onClick={() => setIsMobile(!isMobile)} >
                        {isMobile ? <IoMenu className="text-3xl" /> : <IoClose className="text-3xl " />}
                    </button>
                </div>
                <Menu menuItemStyles={{
                    button: {
                        color: '#52527A',
                        fontSize: 'larger',
                        fontWeight: 'bold',
                        '&:hover': {
                            backgroundColor: '#52527A',
                            color: '#0D0D1A'
                        },
                        [`&.active`]: {
                            backgroundColor: '#13395e',
                            color: '#b6c8d9',
                        },
                    }
                }}>
                    <div className={`flex items-center text-secondary mt-5 mb-3 ${isMobile ? 'justify-center' : 'justify-left ml-3'}`}>
                        <RiMovieLine className="text-3xl" />
                        {!isMobile && (<span className="text-3xl font-bold">Movieasy</span>)}
                    </div>

                    <MenuItemWithState icon={<MdOutlineDashboard />} isActive={activeItem === '/admin/dashboard'} text='Dashboard' href='/admin/dashboard'></MenuItemWithState>
                    <MenuItemWithState icon={<MdOutlineMovie />} isActive={activeItem === '/admin/movies'} text='Movies' href='/admin/movies'></MenuItemWithState>

                    <MenuItem> Logout </MenuItem>
                </Menu>
            </Sidebar>
        </div>
    );
}
