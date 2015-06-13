﻿using System;

namespace Ostis.Sctp.Arguments
{
    /// <summary>
    /// Адрес SC-элемента в памяти.
    /// </summary>
    public struct ScAddress : IArgument
    {
        private ushort segment;
        private ushort offset;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="segment">сегмент</param>
        /// <param name="offset">смещение</param>
        public ScAddress(ushort segment, ushort offset)
        {
            this.segment = segment;
            this.offset = offset;
        }

        /// <summary>
        /// Получает значение адреса из массива байт.
        /// </summary>
        /// <param name="bytes">массив байт</param>
        /// <param name="offset">смещение в массиве</param>
        /// <returns>SC-адрес</returns>
        public static ScAddress Parse(byte[] bytes, int offset)
        {
            var address = new ScAddress();
            if (bytes.Length >= SctpProtocol.ScAddressLength + offset)
            {
                address.segment = BitConverter.ToUInt16(bytes, sizeof(ushort) * 0 + offset);
                address.offset = BitConverter.ToUInt16(bytes, sizeof(ushort) * 1 + offset);
            }
            else
            {
                address.segment = 0;
                address.offset = 0;
            }
            return address;
        }

#warning Что за загадочная хрень со структурами мешает сконвертировать эти 2 свойства в авто-свойства?
        /// <summary>
        /// Сегмент.
        /// </summary>
        public ushort Segment
        {
            get { return segment; }
            set { segment = value; }
        }

        /// <summary>
        /// Смещение.
        /// </summary>
        public ushort Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
		{
		    return string.Format("segment: {0}, offset: {1}", segment, offset);
		}

        #region Реализация интерфеса IArgument

        /// <summary>
        /// Получить массив байт для передачи.
        /// </summary>
        public byte[] GetBytes()
        {
            var bytes = new byte[4];
            Array.Copy(BitConverter.GetBytes(segment), bytes, 2);
            Array.Copy(BitConverter.GetBytes(offset), 0, bytes, 2, 2);
            return bytes;

        }

        #endregion
    }
}
