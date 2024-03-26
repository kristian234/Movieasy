// MenuItemWithState.tsx

import Link from "next/link";
import { MenuItem } from "react-pro-sidebar";
import { ReactNode } from "react";
import { MdOutlineDashboard } from "react-icons/md";
import { IconBase } from "react-icons";

interface Props {
    href: string;
    text: string;
    isActive: boolean;
    icon: any
}

export default function MenuItemWithState({ href, text, isActive, icon }: Props) {
    return (
        <MenuItem
            icon={icon}
            className={`custom-menu-item ${isActive ? 'active' : ''}`}
            component={<Link href={href}></Link>}
        >
            {text}
        </MenuItem>
    );
}
