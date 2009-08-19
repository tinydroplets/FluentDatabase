﻿using System;
using System.IO;

namespace FluentDatabase.SqlServer
{
	public class Constraint : ConstraintBase
	{
		public override void Write( StreamWriter writer )
		{
			if( Type != ConstraintType.NotNull && !string.IsNullOrEmpty( Name ) )
			{
				writer.Write( string.Format( " CONSTRAINT [{0}]", Name ) );
			}
			writer.Write( string.Format( " {0}", GetConstraintType() ) );
		}

		private string GetConstraintType()
		{
			switch( Type )
			{
				case ConstraintType.Check:
					return string.Format( "CHECK {0}", Expression );
				case ConstraintType.ForeignKey:
					return string.Format( "FOREIGN KEY REFERENCES [{0}].[{1}] ( [{2}] )", Schema, Table, Column );
				case ConstraintType.NotNull:
					return "NOT NULL";
				case ConstraintType.PrimaryKey:
					return "PRIMARY KEY";
				case ConstraintType.Unique:
					return "UNIQUE";
				default:
					throw new NotSupportedException( string.Format( "ConstraintType '{0}' is not supported.", Type ) );
			}
		}
	}
}