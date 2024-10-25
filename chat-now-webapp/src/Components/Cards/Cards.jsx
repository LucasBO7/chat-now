import { useEffect } from "react";
import { FaRegTrashAlt } from "react-icons/fa";
import { FaPlus } from "react-icons/fa6";

export const Card = ({ userImg, userName, lastMessage }) => {
    return (
        <div className="flex flex-col gap-4 w-[48%] h-36 p-[10px] bg-[#D9D9D9] rounded-2xl">
            <div className="flex flex-row items-center justify-between gap-2">
                <img className="w-11 h-11 bg-purple rounded-2xl" src={userImg} alt="" />

                <h2 className="w-[78%] font-NotoThai font-bold text-sm">{userName}</h2>
            </div>

            <p className="font-NotoThai text-xs">{lastMessage}</p>
        </div>
    );
}

export const UserCard = ({ user, handleDelete, handleAddFriend, isFriend = false }) => {
    return (
        <div className="flex flex-row items-center justify-between bg-[#D9D9D9] w-full h-20 px-4 rounded-2xl">
            <div className="flex flex-row items-center gap-3 w-[80%] h-full">
                <img className="bg-purple w-11 h-11 rounded-2xl" src={user.photoUrl} alt="" />
                <p className="font-NotoThai font-bold text-sm">{user.name}</p>
            </div>

            <button className="" onClick={isFriend === true ? handleDelete : () => handleAddFriend(user.id)}>
                {
                    isFriend === true
                        ? (<FaRegTrashAlt />)
                        : (<FaPlus color="#615EFC" />)
                }
            </button>
        </div>
    );
}