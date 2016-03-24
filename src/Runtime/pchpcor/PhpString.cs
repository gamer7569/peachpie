﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pchp.Core
{
    /// <summary>
    /// String builder providing fast concatenation and character replacements.
    /// </summary>
    public class PhpString : IPhpConvertible
    {
        #region Fields & Properties

        // TODO: optimize
        // TODO: allow combination of binary string and unicode string
        // TODO: lazy ToString

        StringBuilder _builder;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes empty php string.
        /// </summary>
        /// <param name="capacity">Expected capacity hint.</param>
        public PhpString(int capacity)
        {
            _builder = new StringBuilder(capacity);
        }

        // from builder, binary, unicode, concatenation

        #endregion

        #region Operations

        #region Append

        public void Append(string value)
        {
            _builder.Append(value);
        }

        #endregion

        // Prepend
        // this[] { get; set; }

        #endregion

        #region IPhpConvertible

        public PhpTypeCode TypeCode => PhpTypeCode.WritableString;

        public bool ToBoolean()
        {
            return _builder.Length != 0 && (_builder.Length != 1 || _builder[0] != '0');
        }

        public double ToDouble()
        {
            return Convert.StringToDouble(_builder.ToString());
        }

        public long ToLong()
        {
            return Convert.StringToLongInteger(_builder.ToString());
        }

        public Convert.NumberInfo ToNumber(out PhpNumber number)
        {
            double d;
            long l;
            var info = Convert.StringToNumber(_builder.ToString(), out l, out d);
            number = (info & Convert.NumberInfo.Double) != 0
                ? PhpNumber.Create(d)
                : PhpNumber.Create(l);

            return info;
        }

        public string ToString(Context ctx)
        {
            return _builder.ToString();
        }

        public string ToStringOrThrow(Context ctx) => ToString(ctx);

        #endregion
    }
}
