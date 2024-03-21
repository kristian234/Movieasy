'use client'
import {
    FiChevronDown,
} from "react-icons/fi";

import { IoHomeOutline } from "react-icons/io5";
import { IoLogOutOutline } from "react-icons/io5";
import { IoMenu } from "react-icons/io5";

import { MdOutlineAdminPanelSettings } from "react-icons/md";

import { motion } from "framer-motion";
import { Dispatch, SetStateAction, useState } from "react";
import { IconType } from "react-icons";

export default function UtilityDropdown() {
    const [open, setOpen] = useState(false);

    return (
        <div className="flex items-center justify-center">
            <motion.div animate={open ? "open" : "closed"} className="relative">
                <button
                    onClick={() => setOpen((pv) => !pv)}
                    className="flex items-center gap-2 px-3 py-2 rounded-md text-primary bg-secondary hover:bg-third transition-colors"
                >
                    <span className="font-medium text-xl"><IoMenu /></span>
                    <motion.span variants={iconVariants}>

                        <FiChevronDown />
                    </motion.span>
                </button>

                <motion.ul
                    initial={wrapperVariants.closed}
                    variants={wrapperVariants}
                    style={{ originY: "top", translateX: "-80%" }}
                    className="flex flex-col gap-2 p-2 rounded-lg bg-header shadow-3xl absolute top-[120%] left-[50%] w-48 overflow-hidden"
                >
                    <Option setOpen={setOpen} Icon={IoHomeOutline} text="Home" />
                    <Option setOpen={setOpen} Icon={MdOutlineAdminPanelSettings} text="Admin" />
                    <hr className="border-0 bg-third w-full h-px bg-dropdown-border" />
                    <Option setOpen={setOpen} Icon={IoLogOutOutline} text="Logout" />

                </motion.ul>
            </motion.div>
        </div>
    );
};

const Option = ({
    text,
    Icon,
    setOpen,
}: {
    text: string;
    Icon: IconType;
    setOpen: Dispatch<SetStateAction<boolean>>;
}) => {
    return (
        <motion.li
            variants={itemVariants}
            onClick={() => setOpen(false)}
            className="
                flex
                items-center
                gap-2
                w-full
                p-2
                text-xs
                font-medium
                whitespace-nowrap
                rounded-md hover:bg-secondary text-third hover:text-body transition-colors cursor-pointer"
        >
            <motion.span variants={actionIconVariants}>
                <Icon />
            </motion.span>
            <span>{text}</span>
        </motion.li>
    );
};

const wrapperVariants = {
    open: {
        scaleY: 1,
        transition: {
            when: "beforeChildren",
            staggerChildren: 0.1,
        },
    },
    closed: {
        scaleY: 0,
        transition: {
            when: "afterChildren",
            staggerChildren: 0.1,
        },
    },
};

const iconVariants = {
    open: { rotate: 180 },
    closed: { rotate: 0 },
};

const itemVariants = {
    open: {
        opacity: 1,
        y: 0,
        transition: {
            when: "beforeChildren",
        },
    },
    closed: {
        opacity: 0,
        y: -15,
        transition: {
            when: "afterChildren",
        },
    },
};

const actionIconVariants = {
    open: { scale: 1, y: 0 },
    closed: { scale: 0, y: -7 },
};