'use client'

import { useState } from "react";
import MovieImage from "../movie-image";
import { FaPlay, FaTimes } from "react-icons/fa"; // Import close icon
import ReactPlayer from 'react-player';

interface Props {
    imageUrl: string;
    videoUrl: string;
}

export default function VideoPlayer({ imageUrl, videoUrl }: Props) {
    const [showModal, setShowModal] = useState(false);

    const toggleModal = () => {
        setShowModal(!showModal);
    };

    const closeModal = () => {
        setShowModal(false);
    };

    return (
        <div className={`${showModal ? "" : "ssm:w-full max-w-2xl relative group transition-transform duration-100 hover:scale-95"}`}>

            <div className="h-0 pb-[140%] relative ">
                {showModal ? (
                    <div className="fixed top-0 left-0 w-full h-full flex items-center justify-center z-50 bg-black bg-opacity-75" onClick={closeModal}>
                        <div className="shadow-3xl bg-black bg-opacity-20 p-4 rounded-lg z-50 w-full max-w-3xl">

                            <div className="absolute top-0 right-0 m-3">
                                <button onClick={closeModal} className="bg-gray-800 p-1 rounded-full text-white">
                                    <FaTimes />
                                </button>
                            </div>

                            <div className="relative player-wrapper">
                                <ReactPlayer
                                    url={videoUrl}
                                    controls={true} // Enable default controls
                                    width="100%"
                                    height="100%"
                                    className="react-player absolute inset-0"
                                />
                            </div>
                        </div>
                    </div>
                ) : (
                        <MovieImage src={imageUrl} />
                )}
            </div>

            {!showModal && (
                <div className="absolute inset-0 flex justify-center items-center bg-black bg-opacity-50 cursor-pointer" onClick={toggleModal}>
                    <FaPlay className="h-20 w-20 text-third opacity-80 hover:opacity-100 transition-opacity" />
                </div>
            )}
        </div>
    )
}
