using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XLua;

public partial class LuaManager
{public LuaTable CreatePackBufferBuffer(PackBuffer.Buffer data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("id", data.id);
	t.Set<string, int>("buff_id", data.buff_id);
	t.Set<string, int>("host_aoi_id", data.host_aoi_id);
	t.Set<string, int>("life", data.life);
	t.Set<string, int>("caster_aoi_id", data.caster_aoi_id);
	if (data.param1 != null)
	{
		t.Set<string, int>("param1", data.param1.GetValueOrDefault());
	}
	return t;
}
public PackBuffer.Buffer CreatePBPackBufferBuffer(LuaTable t)
{
	PackBuffer.Buffer data = new PackBuffer.Buffer();
	if( t.ContainsKey<string>("id"))
	{
		data.id = t.Get<string, int>("id");
	}
	else
	{
		Log.LogError("Field id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("buff_id"))
	{
		data.buff_id = t.Get<string, int>("buff_id");
	}
	else
	{
		Log.LogError("Field buff_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("host_aoi_id"))
	{
		data.host_aoi_id = t.Get<string, int>("host_aoi_id");
	}
	else
	{
		Log.LogError("Field host_aoi_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("life"))
	{
		data.life = t.Get<string, int>("life");
	}
	else
	{
		Log.LogError("Field life Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("caster_aoi_id"))
	{
		data.caster_aoi_id = t.Get<string, int>("caster_aoi_id");
	}
	else
	{
		Log.LogError("Field caster_aoi_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("param1"))
	{
		data.param1 = t.Get<string, int>("param1");
	}
	return data;
}
public LuaTable CreatePackBufferBufferUpdate(PackBuffer.BufferUpdate data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("id", data.id);
	t.Set<string, int>("host_aoi_id", data.host_aoi_id);
	if (data.life != null)
	{
		t.Set<string, int>("life", data.life.GetValueOrDefault());
	}
	if (data.param1 != null)
	{
		t.Set<string, int>("param1", data.param1.GetValueOrDefault());
	}
	return t;
}
public PackBuffer.BufferUpdate CreatePBPackBufferBufferUpdate(LuaTable t)
{
	PackBuffer.BufferUpdate data = new PackBuffer.BufferUpdate();
	if( t.ContainsKey<string>("id"))
	{
		data.id = t.Get<string, int>("id");
	}
	else
	{
		Log.LogError("Field id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("host_aoi_id"))
	{
		data.host_aoi_id = t.Get<string, int>("host_aoi_id");
	}
	else
	{
		Log.LogError("Field host_aoi_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("life"))
	{
		data.life = t.Get<string, int>("life");
	}
	if( t.ContainsKey<string>("param1"))
	{
		data.param1 = t.Get<string, int>("param1");
	}
	return data;
}
public LuaTable CreatePackBufferBufferRemove(PackBuffer.BufferRemove data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("id", data.id);
	t.Set<string, int>("host_aoi_id", data.host_aoi_id);
	return t;
}
public PackBuffer.BufferRemove CreatePBPackBufferBufferRemove(LuaTable t)
{
	PackBuffer.BufferRemove data = new PackBuffer.BufferRemove();
	if( t.ContainsKey<string>("id"))
	{
		data.id = t.Get<string, int>("id");
	}
	else
	{
		Log.LogError("Field id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("host_aoi_id"))
	{
		data.host_aoi_id = t.Get<string, int>("host_aoi_id");
	}
	else
	{
		Log.LogError("Field host_aoi_id Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackBufferBufferChange(PackBuffer.BufferChange data)
{
	LuaTable t = luaEnv.NewTable();
	if (data.insert_buffers != null)
	{
		LuaTable insert_buffers_item = luaEnv.NewTable();
		for(int i = 0;i < data.insert_buffers.Count; ++i)
		{
			LuaTable t_insert_buffers = CreatePackBufferBuffer(data.insert_buffers[i]);
			insert_buffers_item.Set<int, LuaTable>(i+1, t_insert_buffers);
		}
		t.Set<string, LuaTable>("insert_buffers", insert_buffers_item);
	}
	if (data.remove_buffers != null)
	{
		LuaTable remove_buffers_item = luaEnv.NewTable();
		for(int i = 0;i < data.remove_buffers.Count; ++i)
		{
			LuaTable t_remove_buffers = CreatePackBufferBufferRemove(data.remove_buffers[i]);
			remove_buffers_item.Set<int, LuaTable>(i+1, t_remove_buffers);
		}
		t.Set<string, LuaTable>("remove_buffers", remove_buffers_item);
	}
	if (data.update_buffers != null)
	{
		LuaTable update_buffers_item = luaEnv.NewTable();
		for(int i = 0;i < data.update_buffers.Count; ++i)
		{
			LuaTable t_update_buffers = CreatePackBufferBufferUpdate(data.update_buffers[i]);
			update_buffers_item.Set<int, LuaTable>(i+1, t_update_buffers);
		}
		t.Set<string, LuaTable>("update_buffers", update_buffers_item);
	}
	return t;
}
public PackBuffer.BufferChange CreatePBPackBufferBufferChange(LuaTable t)
{
	PackBuffer.BufferChange data = new PackBuffer.BufferChange();
	if(t.ContainsKey("insert_buffers"))
	{
		LuaTable t_insert_buffers = t.Get<string, LuaTable>("insert_buffers");
		data.insert_buffers = new List<PackBuffer.Buffer>();
		for(int i = 0; ;++i)
		{
			if(t_insert_buffers.ContainsKey<int>(i + 1))
			{
				LuaTable subT_insert_buffers = t_insert_buffers.Get<int, LuaTable>(i + 1);
				PackBuffer.Buffer subData_insert_buffers = CreatePBPackBufferBuffer(subT_insert_buffers);
				data.insert_buffers.Add(subData_insert_buffers);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("remove_buffers"))
	{
		LuaTable t_remove_buffers = t.Get<string, LuaTable>("remove_buffers");
		data.remove_buffers = new List<PackBuffer.BufferRemove>();
		for(int i = 0; ;++i)
		{
			if(t_remove_buffers.ContainsKey<int>(i + 1))
			{
				LuaTable subT_remove_buffers = t_remove_buffers.Get<int, LuaTable>(i + 1);
				PackBuffer.BufferRemove subData_remove_buffers = CreatePBPackBufferBufferRemove(subT_remove_buffers);
				data.remove_buffers.Add(subData_remove_buffers);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("update_buffers"))
	{
		LuaTable t_update_buffers = t.Get<string, LuaTable>("update_buffers");
		data.update_buffers = new List<PackBuffer.BufferUpdate>();
		for(int i = 0; ;++i)
		{
			if(t_update_buffers.ContainsKey<int>(i + 1))
			{
				LuaTable subT_update_buffers = t_update_buffers.Get<int, LuaTable>(i + 1);
				PackBuffer.BufferUpdate subData_update_buffers = CreatePBPackBufferBufferUpdate(subT_update_buffers);
				data.update_buffers.Add(subData_update_buffers);
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightFightEffect(PackFight.FightEffect data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("max_hp", data.max_hp);
	t.Set<string, int>("max_mp", data.max_mp);
	t.Set<string, int>("attack", data.attack);
	t.Set<string, int>("defence", data.defence);
	t.Set<string, int>("level", data.level);
	t.Set<string, int>("c_skill_id", data.c_skill_id);
	t.Set<string, int>("h_skill_id", data.h_skill_id);
	if (data.skill_levels != null)
	{
		LuaTable skill_levels_item = luaEnv.NewTable();
		for(int i = 0;i < data.skill_levels.Count; ++i)
		{
			skill_levels_item.Set<int, int>(i+1, data.skill_levels[i]);
		}
		t.Set<string, LuaTable>("skill_levels", skill_levels_item);
	}
	return t;
}
public PackFight.FightEffect CreatePBPackFightFightEffect(LuaTable t)
{
	PackFight.FightEffect data = new PackFight.FightEffect();
	if( t.ContainsKey<string>("max_hp"))
	{
		data.max_hp = t.Get<string, int>("max_hp");
	}
	else
	{
		Log.LogError("Field max_hp Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("max_mp"))
	{
		data.max_mp = t.Get<string, int>("max_mp");
	}
	else
	{
		Log.LogError("Field max_mp Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("attack"))
	{
		data.attack = t.Get<string, int>("attack");
	}
	else
	{
		Log.LogError("Field attack Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("defence"))
	{
		data.defence = t.Get<string, int>("defence");
	}
	else
	{
		Log.LogError("Field defence Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("level"))
	{
		data.level = t.Get<string, int>("level");
	}
	else
	{
		Log.LogError("Field level Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("c_skill_id"))
	{
		data.c_skill_id = t.Get<string, int>("c_skill_id");
	}
	else
	{
		Log.LogError("Field c_skill_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("h_skill_id"))
	{
		data.h_skill_id = t.Get<string, int>("h_skill_id");
	}
	else
	{
		Log.LogError("Field h_skill_id Not Exist in LuaTable From Service!!");
	}
	if(t.ContainsKey("skill_levels"))
	{
		LuaTable t_skill_levels = t.Get<string, LuaTable>("skill_levels");
		data.skill_levels = new List<int>();
		for(int i = 0; ;++i)
		{
			if(t_skill_levels.ContainsKey<int>(i + 1))
			{
				data.skill_levels.Add(t_skill_levels.Get<int, int>(i + 1));
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightFightObject(PackFight.FightObject data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("id", data.id);
	t.Set<string, int>("obj_type", data.obj_type);
	t.Set<string, int>("base_id", data.base_id);
	t.Set<string, int>("position", data.position);
	t.Set<string, System.Int64>("hp", data.hp);
	t.Set<string, int>("mp", data.mp);
	t.Set<string, int>("c_turn", data.c_turn);
	t.Set<string, int>("wave_id", data.wave_id);
	LuaTable t_FightEffect = CreatePackFightFightEffect(data.effect);
	t.Set<string, LuaTable>("effect", t_FightEffect);
	return t;
}
public PackFight.FightObject CreatePBPackFightFightObject(LuaTable t)
{
	PackFight.FightObject data = new PackFight.FightObject();
	if( t.ContainsKey<string>("id"))
	{
		data.id = t.Get<string, int>("id");
	}
	else
	{
		Log.LogError("Field id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("obj_type"))
	{
		data.obj_type = t.Get<string, int>("obj_type");
	}
	else
	{
		Log.LogError("Field obj_type Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("base_id"))
	{
		data.base_id = t.Get<string, int>("base_id");
	}
	else
	{
		Log.LogError("Field base_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("position"))
	{
		data.position = t.Get<string, int>("position");
	}
	else
	{
		Log.LogError("Field position Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("hp"))
	{
		data.hp = t.Get<string, System.Int64>("hp");
	}
	else
	{
		Log.LogError("Field hp Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("mp"))
	{
		data.mp = t.Get<string, int>("mp");
	}
	else
	{
		Log.LogError("Field mp Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("c_turn"))
	{
		data.c_turn = t.Get<string, int>("c_turn");
	}
	else
	{
		Log.LogError("Field c_turn Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("wave_id"))
	{
		data.wave_id = t.Get<string, int>("wave_id");
	}
	else
	{
		Log.LogError("Field wave_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("effect"))
	{
		LuaTable subTable_effect = t.Get<string, LuaTable>("effect");
		data.effect = CreatePBPackFightFightEffect(subTable_effect);
	}
	else
	{
		Log.LogError("Field effect Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackFightFightObjectUpdate(PackFight.FightObjectUpdate data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("id", data.id);
	if (data.hp != null)
	{
		t.Set<string, System.Int64>("hp", data.hp.GetValueOrDefault());
	}
	if (data.mp != null)
	{
		t.Set<string, int>("mp", data.mp.GetValueOrDefault());
	}
	if (data.c_turn != null)
	{
		t.Set<string, int>("c_turn", data.c_turn.GetValueOrDefault());
	}
	return t;
}
public PackFight.FightObjectUpdate CreatePBPackFightFightObjectUpdate(LuaTable t)
{
	PackFight.FightObjectUpdate data = new PackFight.FightObjectUpdate();
	if( t.ContainsKey<string>("id"))
	{
		data.id = t.Get<string, int>("id");
	}
	else
	{
		Log.LogError("Field id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("hp"))
	{
		data.hp = t.Get<string, System.Int64>("hp");
	}
	if( t.ContainsKey<string>("mp"))
	{
		data.mp = t.Get<string, int>("mp");
	}
	if( t.ContainsKey<string>("c_turn"))
	{
		data.c_turn = t.Get<string, int>("c_turn");
	}
	return data;
}
public LuaTable CreatePackFightBeforeUpdate(PackFight.BeforeUpdate data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("id", data.id);
	return t;
}
public PackFight.BeforeUpdate CreatePBPackFightBeforeUpdate(LuaTable t)
{
	PackFight.BeforeUpdate data = new PackFight.BeforeUpdate();
	if( t.ContainsKey<string>("id"))
	{
		data.id = t.Get<string, int>("id");
	}
	else
	{
		Log.LogError("Field id Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackFightAfterUpdate(PackFight.AfterUpdate data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("id", data.id);
	if (data.add_mp != null)
	{
		t.Set<string, int>("add_mp", data.add_mp.GetValueOrDefault());
	}
	return t;
}
public PackFight.AfterUpdate CreatePBPackFightAfterUpdate(LuaTable t)
{
	PackFight.AfterUpdate data = new PackFight.AfterUpdate();
	if( t.ContainsKey<string>("id"))
	{
		data.id = t.Get<string, int>("id");
	}
	else
	{
		Log.LogError("Field id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("add_mp"))
	{
		data.add_mp = t.Get<string, int>("add_mp");
	}
	return data;
}
public LuaTable CreatePackFightCleanHarmUnit(PackFight.CleanHarmUnit data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("target_id", data.target_id);
	t.Set<string, System.Int64>("hp_change", data.hp_change);
	if (data.retinue != null)
	{
		LuaTable t_RetinueChange = CreatePackRetinueRetinueChange(data.retinue);
		t.Set<string, LuaTable>("retinue", t_RetinueChange);
	}
	t.Set<string, int>("harm_type", data.harm_type);
	return t;
}
public PackFight.CleanHarmUnit CreatePBPackFightCleanHarmUnit(LuaTable t)
{
	PackFight.CleanHarmUnit data = new PackFight.CleanHarmUnit();
	if( t.ContainsKey<string>("target_id"))
	{
		data.target_id = t.Get<string, int>("target_id");
	}
	else
	{
		Log.LogError("Field target_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("hp_change"))
	{
		data.hp_change = t.Get<string, System.Int64>("hp_change");
	}
	else
	{
		Log.LogError("Field hp_change Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("retinue"))
	{
		LuaTable subTable_retinue = t.Get<string, LuaTable>("retinue");
		data.retinue = CreatePBPackRetinueRetinueChange(subTable_retinue);
	}
	if( t.ContainsKey<string>("harm_type"))
	{
		data.harm_type = t.Get<string, int>("harm_type");
	}
	else
	{
		Log.LogError("Field harm_type Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackFightCleanHarm(PackFight.CleanHarm data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("zhu_position", data.zhu_position);
	t.Set<string, int>("target_row", data.target_row);
	if (data.harms != null)
	{
		LuaTable harms_item = luaEnv.NewTable();
		for(int i = 0;i < data.harms.Count; ++i)
		{
			LuaTable t_harms = CreatePackFightCleanHarmUnit(data.harms[i]);
			harms_item.Set<int, LuaTable>(i+1, t_harms);
		}
		t.Set<string, LuaTable>("harms", harms_item);
	}
	return t;
}
public PackFight.CleanHarm CreatePBPackFightCleanHarm(LuaTable t)
{
	PackFight.CleanHarm data = new PackFight.CleanHarm();
	if( t.ContainsKey<string>("zhu_position"))
	{
		data.zhu_position = t.Get<string, int>("zhu_position");
	}
	else
	{
		Log.LogError("Field zhu_position Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("target_row"))
	{
		data.target_row = t.Get<string, int>("target_row");
	}
	else
	{
		Log.LogError("Field target_row Not Exist in LuaTable From Service!!");
	}
	if(t.ContainsKey("harms"))
	{
		LuaTable t_harms = t.Get<string, LuaTable>("harms");
		data.harms = new List<PackFight.CleanHarmUnit>();
		for(int i = 0; ;++i)
		{
			if(t_harms.ContainsKey<int>(i + 1))
			{
				LuaTable subT_harms = t_harms.Get<int, LuaTable>(i + 1);
				PackFight.CleanHarmUnit subData_harms = CreatePBPackFightCleanHarmUnit(subT_harms);
				data.harms.Add(subData_harms);
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightMatchUnit(PackFight.MatchUnit data)
{
	LuaTable t = luaEnv.NewTable();
	if (data.positions != null)
	{
		LuaTable positions_item = luaEnv.NewTable();
		for(int i = 0;i < data.positions.Count; ++i)
		{
			positions_item.Set<int, int>(i+1, data.positions[i]);
		}
		t.Set<string, LuaTable>("positions", positions_item);
	}
	t.Set<string, int>("match_type", data.match_type);
	return t;
}
public PackFight.MatchUnit CreatePBPackFightMatchUnit(LuaTable t)
{
	PackFight.MatchUnit data = new PackFight.MatchUnit();
	if(t.ContainsKey("positions"))
	{
		LuaTable t_positions = t.Get<string, LuaTable>("positions");
		data.positions = new List<int>();
		for(int i = 0; ;++i)
		{
			if(t_positions.ContainsKey<int>(i + 1))
			{
				data.positions.Add(t_positions.Get<int, int>(i + 1));
			}
			else
			{
				break;
			}
		}
	}
	if( t.ContainsKey<string>("match_type"))
	{
		data.match_type = t.Get<string, int>("match_type");
	}
	else
	{
		Log.LogError("Field match_type Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackFightCleanCombo(PackFight.CleanCombo data)
{
	LuaTable t = luaEnv.NewTable();
	if (data.match_infos != null)
	{
		LuaTable match_infos_item = luaEnv.NewTable();
		for(int i = 0;i < data.match_infos.Count; ++i)
		{
			LuaTable t_match_infos = CreatePackFightMatchUnit(data.match_infos[i]);
			match_infos_item.Set<int, LuaTable>(i+1, t_match_infos);
		}
		t.Set<string, LuaTable>("match_infos", match_infos_item);
	}
	return t;
}
public PackFight.CleanCombo CreatePBPackFightCleanCombo(LuaTable t)
{
	PackFight.CleanCombo data = new PackFight.CleanCombo();
	if(t.ContainsKey("match_infos"))
	{
		LuaTable t_match_infos = t.Get<string, LuaTable>("match_infos");
		data.match_infos = new List<PackFight.MatchUnit>();
		for(int i = 0; ;++i)
		{
			if(t_match_infos.ContainsKey<int>(i + 1))
			{
				LuaTable subT_match_infos = t_match_infos.Get<int, LuaTable>(i + 1);
				PackFight.MatchUnit subData_match_infos = CreatePBPackFightMatchUnit(subT_match_infos);
				data.match_infos.Add(subData_match_infos);
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightSpecailEffect(PackFight.SpecailEffect data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("effect_type", data.effect_type);
	if (data.effect_params != null)
	{
		LuaTable effect_params_item = luaEnv.NewTable();
		for(int i = 0;i < data.effect_params.Count; ++i)
		{
			effect_params_item.Set<int, int>(i+1, data.effect_params[i]);
		}
		t.Set<string, LuaTable>("effect_params", effect_params_item);
	}
	return t;
}
public PackFight.SpecailEffect CreatePBPackFightSpecailEffect(LuaTable t)
{
	PackFight.SpecailEffect data = new PackFight.SpecailEffect();
	if( t.ContainsKey<string>("effect_type"))
	{
		data.effect_type = t.Get<string, int>("effect_type");
	}
	else
	{
		Log.LogError("Field effect_type Not Exist in LuaTable From Service!!");
	}
	if(t.ContainsKey("effect_params"))
	{
		LuaTable t_effect_params = t.Get<string, LuaTable>("effect_params");
		data.effect_params = new List<int>();
		for(int i = 0; ;++i)
		{
			if(t_effect_params.ContainsKey<int>(i + 1))
			{
				data.effect_params.Add(t_effect_params.Get<int, int>(i + 1));
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightSkillEffect(PackFight.SkillEffect data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("target_id", data.target_id);
	if (data.hp_change != null)
	{
		t.Set<string, System.Int64>("hp_change", data.hp_change.GetValueOrDefault());
	}
	if (data.mp_change != null)
	{
		t.Set<string, int>("mp_change", data.mp_change.GetValueOrDefault());
	}
	if (data.buffer != null)
	{
		LuaTable t_BufferChange = CreatePackBufferBufferChange(data.buffer);
		t.Set<string, LuaTable>("buffer", t_BufferChange);
	}
	if (data.retinue != null)
	{
		LuaTable t_RetinueChange = CreatePackRetinueRetinueChange(data.retinue);
		t.Set<string, LuaTable>("retinue", t_RetinueChange);
	}
	if (data.extra_effect != null)
	{
		LuaTable t_SpecailEffect = CreatePackFightSpecailEffect(data.extra_effect);
		t.Set<string, LuaTable>("extra_effect", t_SpecailEffect);
	}
	if (data.skill_effect_type != null)
	{
		t.Set<string, int>("skill_effect_type", data.skill_effect_type.GetValueOrDefault());
	}
	if (data.fight_section != null)
	{
		t.Set<string, int>("fight_section", data.fight_section.GetValueOrDefault());
	}
	return t;
}
public PackFight.SkillEffect CreatePBPackFightSkillEffect(LuaTable t)
{
	PackFight.SkillEffect data = new PackFight.SkillEffect();
	if( t.ContainsKey<string>("target_id"))
	{
		data.target_id = t.Get<string, int>("target_id");
	}
	else
	{
		Log.LogError("Field target_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("hp_change"))
	{
		data.hp_change = t.Get<string, System.Int64>("hp_change");
	}
	if( t.ContainsKey<string>("mp_change"))
	{
		data.mp_change = t.Get<string, int>("mp_change");
	}
	if( t.ContainsKey<string>("buffer"))
	{
		LuaTable subTable_buffer = t.Get<string, LuaTable>("buffer");
		data.buffer = CreatePBPackBufferBufferChange(subTable_buffer);
	}
	if( t.ContainsKey<string>("retinue"))
	{
		LuaTable subTable_retinue = t.Get<string, LuaTable>("retinue");
		data.retinue = CreatePBPackRetinueRetinueChange(subTable_retinue);
	}
	if( t.ContainsKey<string>("extra_effect"))
	{
		LuaTable subTable_extra_effect = t.Get<string, LuaTable>("extra_effect");
		data.extra_effect = CreatePBPackFightSpecailEffect(subTable_extra_effect);
	}
	if( t.ContainsKey<string>("skill_effect_type"))
	{
		data.skill_effect_type = t.Get<string, int>("skill_effect_type");
	}
	if( t.ContainsKey<string>("fight_section"))
	{
		data.fight_section = t.Get<string, int>("fight_section");
	}
	return data;
}
public LuaTable CreatePackFightBufferEffect(PackFight.BufferEffect data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("id", data.id);
	t.Set<string, int>("target_id", data.target_id);
	if (data.hp_change != null)
	{
		t.Set<string, System.Int64>("hp_change", data.hp_change.GetValueOrDefault());
	}
	if (data.mp_change != null)
	{
		t.Set<string, int>("mp_change", data.mp_change.GetValueOrDefault());
	}
	return t;
}
public PackFight.BufferEffect CreatePBPackFightBufferEffect(LuaTable t)
{
	PackFight.BufferEffect data = new PackFight.BufferEffect();
	if( t.ContainsKey<string>("id"))
	{
		data.id = t.Get<string, int>("id");
	}
	else
	{
		Log.LogError("Field id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("target_id"))
	{
		data.target_id = t.Get<string, int>("target_id");
	}
	else
	{
		Log.LogError("Field target_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("hp_change"))
	{
		data.hp_change = t.Get<string, System.Int64>("hp_change");
	}
	if( t.ContainsKey<string>("mp_change"))
	{
		data.mp_change = t.Get<string, int>("mp_change");
	}
	return data;
}
public LuaTable CreatePackFightFightOpUseSkill(PackFight.FightOpUseSkill data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("fighter_aoi_id", data.fighter_aoi_id);
	t.Set<string, int>("skill_id", data.skill_id);
	if (data.before != null)
	{
		LuaTable before_item = luaEnv.NewTable();
		for(int i = 0;i < data.before.Count; ++i)
		{
			LuaTable t_before = CreatePackFightSkillEffect(data.before[i]);
			before_item.Set<int, LuaTable>(i+1, t_before);
		}
		t.Set<string, LuaTable>("before", before_item);
	}
	if (data.between != null)
	{
		LuaTable between_item = luaEnv.NewTable();
		for(int i = 0;i < data.between.Count; ++i)
		{
			LuaTable t_between = CreatePackFightSkillEffect(data.between[i]);
			between_item.Set<int, LuaTable>(i+1, t_between);
		}
		t.Set<string, LuaTable>("between", between_item);
	}
	if (data.after != null)
	{
		LuaTable after_item = luaEnv.NewTable();
		for(int i = 0;i < data.after.Count; ++i)
		{
			LuaTable t_after = CreatePackFightSkillEffect(data.after[i]);
			after_item.Set<int, LuaTable>(i+1, t_after);
		}
		t.Set<string, LuaTable>("after", after_item);
	}
	if (data.c_turn != null)
	{
		t.Set<string, int>("c_turn", data.c_turn.GetValueOrDefault());
	}
	if (data.is_extra != null)
	{
		t.Set<string, bool>("is_extra", data.is_extra.GetValueOrDefault());
	}
	return t;
}
public PackFight.FightOpUseSkill CreatePBPackFightFightOpUseSkill(LuaTable t)
{
	PackFight.FightOpUseSkill data = new PackFight.FightOpUseSkill();
	if( t.ContainsKey<string>("fighter_aoi_id"))
	{
		data.fighter_aoi_id = t.Get<string, int>("fighter_aoi_id");
	}
	else
	{
		Log.LogError("Field fighter_aoi_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("skill_id"))
	{
		data.skill_id = t.Get<string, int>("skill_id");
	}
	else
	{
		Log.LogError("Field skill_id Not Exist in LuaTable From Service!!");
	}
	if(t.ContainsKey("before"))
	{
		LuaTable t_before = t.Get<string, LuaTable>("before");
		data.before = new List<PackFight.SkillEffect>();
		for(int i = 0; ;++i)
		{
			if(t_before.ContainsKey<int>(i + 1))
			{
				LuaTable subT_before = t_before.Get<int, LuaTable>(i + 1);
				PackFight.SkillEffect subData_before = CreatePBPackFightSkillEffect(subT_before);
				data.before.Add(subData_before);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("between"))
	{
		LuaTable t_between = t.Get<string, LuaTable>("between");
		data.between = new List<PackFight.SkillEffect>();
		for(int i = 0; ;++i)
		{
			if(t_between.ContainsKey<int>(i + 1))
			{
				LuaTable subT_between = t_between.Get<int, LuaTable>(i + 1);
				PackFight.SkillEffect subData_between = CreatePBPackFightSkillEffect(subT_between);
				data.between.Add(subData_between);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("after"))
	{
		LuaTable t_after = t.Get<string, LuaTable>("after");
		data.after = new List<PackFight.SkillEffect>();
		for(int i = 0; ;++i)
		{
			if(t_after.ContainsKey<int>(i + 1))
			{
				LuaTable subT_after = t_after.Get<int, LuaTable>(i + 1);
				PackFight.SkillEffect subData_after = CreatePBPackFightSkillEffect(subT_after);
				data.after.Add(subData_after);
			}
			else
			{
				break;
			}
		}
	}
	if( t.ContainsKey<string>("c_turn"))
	{
		data.c_turn = t.Get<string, int>("c_turn");
	}
	if( t.ContainsKey<string>("is_extra"))
	{
		data.is_extra = t.Get<string, bool>("is_extra");
	}
	return data;
}
public LuaTable CreatePackFightFightSuppply(PackFight.FightSuppply data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("line", data.line);
	if (data.colors != null)
	{
		LuaTable colors_item = luaEnv.NewTable();
		for(int i = 0;i < data.colors.Count; ++i)
		{
			colors_item.Set<int, int>(i+1, data.colors[i]);
		}
		t.Set<string, LuaTable>("colors", colors_item);
	}
	return t;
}
public PackFight.FightSuppply CreatePBPackFightFightSuppply(LuaTable t)
{
	PackFight.FightSuppply data = new PackFight.FightSuppply();
	if( t.ContainsKey<string>("line"))
	{
		data.line = t.Get<string, int>("line");
	}
	else
	{
		Log.LogError("Field line Not Exist in LuaTable From Service!!");
	}
	if(t.ContainsKey("colors"))
	{
		LuaTable t_colors = t.Get<string, LuaTable>("colors");
		data.colors = new List<int>();
		for(int i = 0; ;++i)
		{
			if(t_colors.ContainsKey<int>(i + 1))
			{
				data.colors.Add(t_colors.Get<int, int>(i + 1));
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightFightOpSwap(PackFight.FightOpSwap data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("first", data.first);
	if (data.second != null)
	{
		t.Set<string, int>("second", data.second.GetValueOrDefault());
	}
	if (data.harms != null)
	{
		LuaTable harms_item = luaEnv.NewTable();
		for(int i = 0;i < data.harms.Count; ++i)
		{
			LuaTable t_harms = CreatePackFightCleanHarm(data.harms[i]);
			harms_item.Set<int, LuaTable>(i+1, t_harms);
		}
		t.Set<string, LuaTable>("harms", harms_item);
	}
	if (data.clean_combos != null)
	{
		LuaTable clean_combos_item = luaEnv.NewTable();
		for(int i = 0;i < data.clean_combos.Count; ++i)
		{
			LuaTable t_clean_combos = CreatePackFightCleanCombo(data.clean_combos[i]);
			clean_combos_item.Set<int, LuaTable>(i+1, t_clean_combos);
		}
		t.Set<string, LuaTable>("clean_combos", clean_combos_item);
	}
	if (data.bonus != null)
	{
		LuaTable bonus_item = luaEnv.NewTable();
		for(int i = 0;i < data.bonus.Count; ++i)
		{
			LuaTable t_bonus = CreatePackFightPluzzlesItem(data.bonus[i]);
			bonus_item.Set<int, LuaTable>(i+1, t_bonus);
		}
		t.Set<string, LuaTable>("bonus", bonus_item);
	}
	if (data.buffer != null)
	{
		LuaTable buffer_item = luaEnv.NewTable();
		for(int i = 0;i < data.buffer.Count; ++i)
		{
			LuaTable t_buffer = CreatePackBufferBufferChange(data.buffer[i]);
			buffer_item.Set<int, LuaTable>(i+1, t_buffer);
		}
		t.Set<string, LuaTable>("buffer", buffer_item);
	}
	return t;
}
public PackFight.FightOpSwap CreatePBPackFightFightOpSwap(LuaTable t)
{
	PackFight.FightOpSwap data = new PackFight.FightOpSwap();
	if( t.ContainsKey<string>("first"))
	{
		data.first = t.Get<string, int>("first");
	}
	else
	{
		Log.LogError("Field first Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("second"))
	{
		data.second = t.Get<string, int>("second");
	}
	if(t.ContainsKey("harms"))
	{
		LuaTable t_harms = t.Get<string, LuaTable>("harms");
		data.harms = new List<PackFight.CleanHarm>();
		for(int i = 0; ;++i)
		{
			if(t_harms.ContainsKey<int>(i + 1))
			{
				LuaTable subT_harms = t_harms.Get<int, LuaTable>(i + 1);
				PackFight.CleanHarm subData_harms = CreatePBPackFightCleanHarm(subT_harms);
				data.harms.Add(subData_harms);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("clean_combos"))
	{
		LuaTable t_clean_combos = t.Get<string, LuaTable>("clean_combos");
		data.clean_combos = new List<PackFight.CleanCombo>();
		for(int i = 0; ;++i)
		{
			if(t_clean_combos.ContainsKey<int>(i + 1))
			{
				LuaTable subT_clean_combos = t_clean_combos.Get<int, LuaTable>(i + 1);
				PackFight.CleanCombo subData_clean_combos = CreatePBPackFightCleanCombo(subT_clean_combos);
				data.clean_combos.Add(subData_clean_combos);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("bonus"))
	{
		LuaTable t_bonus = t.Get<string, LuaTable>("bonus");
		data.bonus = new List<PackFight.PluzzlesItem>();
		for(int i = 0; ;++i)
		{
			if(t_bonus.ContainsKey<int>(i + 1))
			{
				LuaTable subT_bonus = t_bonus.Get<int, LuaTable>(i + 1);
				PackFight.PluzzlesItem subData_bonus = CreatePBPackFightPluzzlesItem(subT_bonus);
				data.bonus.Add(subData_bonus);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("buffer"))
	{
		LuaTable t_buffer = t.Get<string, LuaTable>("buffer");
		data.buffer = new List<PackBuffer.BufferChange>();
		for(int i = 0; ;++i)
		{
			if(t_buffer.ContainsKey<int>(i + 1))
			{
				LuaTable subT_buffer = t_buffer.Get<int, LuaTable>(i + 1);
				PackBuffer.BufferChange subData_buffer = CreatePBPackBufferBufferChange(subT_buffer);
				data.buffer.Add(subData_buffer);
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightFightOpSupply(PackFight.FightOpSupply data)
{
	LuaTable t = luaEnv.NewTable();
	if (data.supplys != null)
	{
		LuaTable supplys_item = luaEnv.NewTable();
		for(int i = 0;i < data.supplys.Count; ++i)
		{
			LuaTable t_supplys = CreatePackFightFightSuppply(data.supplys[i]);
			supplys_item.Set<int, LuaTable>(i+1, t_supplys);
		}
		t.Set<string, LuaTable>("supplys", supplys_item);
	}
	if (data.harms != null)
	{
		LuaTable harms_item = luaEnv.NewTable();
		for(int i = 0;i < data.harms.Count; ++i)
		{
			LuaTable t_harms = CreatePackFightCleanHarm(data.harms[i]);
			harms_item.Set<int, LuaTable>(i+1, t_harms);
		}
		t.Set<string, LuaTable>("harms", harms_item);
	}
	if (data.clean_combos != null)
	{
		LuaTable clean_combos_item = luaEnv.NewTable();
		for(int i = 0;i < data.clean_combos.Count; ++i)
		{
			LuaTable t_clean_combos = CreatePackFightCleanCombo(data.clean_combos[i]);
			clean_combos_item.Set<int, LuaTable>(i+1, t_clean_combos);
		}
		t.Set<string, LuaTable>("clean_combos", clean_combos_item);
	}
	if (data.bonus != null)
	{
		LuaTable bonus_item = luaEnv.NewTable();
		for(int i = 0;i < data.bonus.Count; ++i)
		{
			LuaTable t_bonus = CreatePackFightPluzzlesItem(data.bonus[i]);
			bonus_item.Set<int, LuaTable>(i+1, t_bonus);
		}
		t.Set<string, LuaTable>("bonus", bonus_item);
	}
	if (data.buffer != null)
	{
		LuaTable buffer_item = luaEnv.NewTable();
		for(int i = 0;i < data.buffer.Count; ++i)
		{
			LuaTable t_buffer = CreatePackBufferBufferChange(data.buffer[i]);
			buffer_item.Set<int, LuaTable>(i+1, t_buffer);
		}
		t.Set<string, LuaTable>("buffer", buffer_item);
	}
	return t;
}
public PackFight.FightOpSupply CreatePBPackFightFightOpSupply(LuaTable t)
{
	PackFight.FightOpSupply data = new PackFight.FightOpSupply();
	if(t.ContainsKey("supplys"))
	{
		LuaTable t_supplys = t.Get<string, LuaTable>("supplys");
		data.supplys = new List<PackFight.FightSuppply>();
		for(int i = 0; ;++i)
		{
			if(t_supplys.ContainsKey<int>(i + 1))
			{
				LuaTable subT_supplys = t_supplys.Get<int, LuaTable>(i + 1);
				PackFight.FightSuppply subData_supplys = CreatePBPackFightFightSuppply(subT_supplys);
				data.supplys.Add(subData_supplys);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("harms"))
	{
		LuaTable t_harms = t.Get<string, LuaTable>("harms");
		data.harms = new List<PackFight.CleanHarm>();
		for(int i = 0; ;++i)
		{
			if(t_harms.ContainsKey<int>(i + 1))
			{
				LuaTable subT_harms = t_harms.Get<int, LuaTable>(i + 1);
				PackFight.CleanHarm subData_harms = CreatePBPackFightCleanHarm(subT_harms);
				data.harms.Add(subData_harms);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("clean_combos"))
	{
		LuaTable t_clean_combos = t.Get<string, LuaTable>("clean_combos");
		data.clean_combos = new List<PackFight.CleanCombo>();
		for(int i = 0; ;++i)
		{
			if(t_clean_combos.ContainsKey<int>(i + 1))
			{
				LuaTable subT_clean_combos = t_clean_combos.Get<int, LuaTable>(i + 1);
				PackFight.CleanCombo subData_clean_combos = CreatePBPackFightCleanCombo(subT_clean_combos);
				data.clean_combos.Add(subData_clean_combos);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("bonus"))
	{
		LuaTable t_bonus = t.Get<string, LuaTable>("bonus");
		data.bonus = new List<PackFight.PluzzlesItem>();
		for(int i = 0; ;++i)
		{
			if(t_bonus.ContainsKey<int>(i + 1))
			{
				LuaTable subT_bonus = t_bonus.Get<int, LuaTable>(i + 1);
				PackFight.PluzzlesItem subData_bonus = CreatePBPackFightPluzzlesItem(subT_bonus);
				data.bonus.Add(subData_bonus);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("buffer"))
	{
		LuaTable t_buffer = t.Get<string, LuaTable>("buffer");
		data.buffer = new List<PackBuffer.BufferChange>();
		for(int i = 0; ;++i)
		{
			if(t_buffer.ContainsKey<int>(i + 1))
			{
				LuaTable subT_buffer = t_buffer.Get<int, LuaTable>(i + 1);
				PackBuffer.BufferChange subData_buffer = CreatePBPackBufferBufferChange(subT_buffer);
				data.buffer.Add(subData_buffer);
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightFightOpXipai(PackFight.FightOpXipai data)
{
	LuaTable t = luaEnv.NewTable();
	if (data.xipai_items != null)
	{
		LuaTable xipai_items_item = luaEnv.NewTable();
		for(int i = 0;i < data.xipai_items.Count; ++i)
		{
			LuaTable t_xipai_items = CreatePackFightPluzzlesItem(data.xipai_items[i]);
			xipai_items_item.Set<int, LuaTable>(i+1, t_xipai_items);
		}
		t.Set<string, LuaTable>("xipai_items", xipai_items_item);
	}
	return t;
}
public PackFight.FightOpXipai CreatePBPackFightFightOpXipai(LuaTable t)
{
	PackFight.FightOpXipai data = new PackFight.FightOpXipai();
	if(t.ContainsKey("xipai_items"))
	{
		LuaTable t_xipai_items = t.Get<string, LuaTable>("xipai_items");
		data.xipai_items = new List<PackFight.PluzzlesItem>();
		for(int i = 0; ;++i)
		{
			if(t_xipai_items.ContainsKey<int>(i + 1))
			{
				LuaTable subT_xipai_items = t_xipai_items.Get<int, LuaTable>(i + 1);
				PackFight.PluzzlesItem subData_xipai_items = CreatePBPackFightPluzzlesItem(subT_xipai_items);
				data.xipai_items.Add(subData_xipai_items);
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightFightOpBefore(PackFight.FightOpBefore data)
{
	LuaTable t = luaEnv.NewTable();
	if (data.buffer_effects != null)
	{
		LuaTable buffer_effects_item = luaEnv.NewTable();
		for(int i = 0;i < data.buffer_effects.Count; ++i)
		{
			LuaTable t_buffer_effects = CreatePackFightBufferEffect(data.buffer_effects[i]);
			buffer_effects_item.Set<int, LuaTable>(i+1, t_buffer_effects);
		}
		t.Set<string, LuaTable>("buffer_effects", buffer_effects_item);
	}
	if (data.before_update != null)
	{
		LuaTable before_update_item = luaEnv.NewTable();
		for(int i = 0;i < data.before_update.Count; ++i)
		{
			LuaTable t_before_update = CreatePackFightBeforeUpdate(data.before_update[i]);
			before_update_item.Set<int, LuaTable>(i+1, t_before_update);
		}
		t.Set<string, LuaTable>("before_update", before_update_item);
	}
	if (data.buffer != null)
	{
		LuaTable t_BufferChange = CreatePackBufferBufferChange(data.buffer);
		t.Set<string, LuaTable>("buffer", t_BufferChange);
	}
	return t;
}
public PackFight.FightOpBefore CreatePBPackFightFightOpBefore(LuaTable t)
{
	PackFight.FightOpBefore data = new PackFight.FightOpBefore();
	if(t.ContainsKey("buffer_effects"))
	{
		LuaTable t_buffer_effects = t.Get<string, LuaTable>("buffer_effects");
		data.buffer_effects = new List<PackFight.BufferEffect>();
		for(int i = 0; ;++i)
		{
			if(t_buffer_effects.ContainsKey<int>(i + 1))
			{
				LuaTable subT_buffer_effects = t_buffer_effects.Get<int, LuaTable>(i + 1);
				PackFight.BufferEffect subData_buffer_effects = CreatePBPackFightBufferEffect(subT_buffer_effects);
				data.buffer_effects.Add(subData_buffer_effects);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("before_update"))
	{
		LuaTable t_before_update = t.Get<string, LuaTable>("before_update");
		data.before_update = new List<PackFight.BeforeUpdate>();
		for(int i = 0; ;++i)
		{
			if(t_before_update.ContainsKey<int>(i + 1))
			{
				LuaTable subT_before_update = t_before_update.Get<int, LuaTable>(i + 1);
				PackFight.BeforeUpdate subData_before_update = CreatePBPackFightBeforeUpdate(subT_before_update);
				data.before_update.Add(subData_before_update);
			}
			else
			{
				break;
			}
		}
	}
	if( t.ContainsKey<string>("buffer"))
	{
		LuaTable subTable_buffer = t.Get<string, LuaTable>("buffer");
		data.buffer = CreatePBPackBufferBufferChange(subTable_buffer);
	}
	return data;
}
public LuaTable CreatePackFightFightOpAfter(PackFight.FightOpAfter data)
{
	LuaTable t = luaEnv.NewTable();
	if (data.buffer_effects != null)
	{
		LuaTable buffer_effects_item = luaEnv.NewTable();
		for(int i = 0;i < data.buffer_effects.Count; ++i)
		{
			LuaTable t_buffer_effects = CreatePackFightBufferEffect(data.buffer_effects[i]);
			buffer_effects_item.Set<int, LuaTable>(i+1, t_buffer_effects);
		}
		t.Set<string, LuaTable>("buffer_effects", buffer_effects_item);
	}
	if (data.after_update != null)
	{
		LuaTable after_update_item = luaEnv.NewTable();
		for(int i = 0;i < data.after_update.Count; ++i)
		{
			LuaTable t_after_update = CreatePackFightAfterUpdate(data.after_update[i]);
			after_update_item.Set<int, LuaTable>(i+1, t_after_update);
		}
		t.Set<string, LuaTable>("after_update", after_update_item);
	}
	if (data.buffer != null)
	{
		LuaTable t_BufferChange = CreatePackBufferBufferChange(data.buffer);
		t.Set<string, LuaTable>("buffer", t_BufferChange);
	}
	return t;
}
public PackFight.FightOpAfter CreatePBPackFightFightOpAfter(LuaTable t)
{
	PackFight.FightOpAfter data = new PackFight.FightOpAfter();
	if(t.ContainsKey("buffer_effects"))
	{
		LuaTable t_buffer_effects = t.Get<string, LuaTable>("buffer_effects");
		data.buffer_effects = new List<PackFight.BufferEffect>();
		for(int i = 0; ;++i)
		{
			if(t_buffer_effects.ContainsKey<int>(i + 1))
			{
				LuaTable subT_buffer_effects = t_buffer_effects.Get<int, LuaTable>(i + 1);
				PackFight.BufferEffect subData_buffer_effects = CreatePBPackFightBufferEffect(subT_buffer_effects);
				data.buffer_effects.Add(subData_buffer_effects);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("after_update"))
	{
		LuaTable t_after_update = t.Get<string, LuaTable>("after_update");
		data.after_update = new List<PackFight.AfterUpdate>();
		for(int i = 0; ;++i)
		{
			if(t_after_update.ContainsKey<int>(i + 1))
			{
				LuaTable subT_after_update = t_after_update.Get<int, LuaTable>(i + 1);
				PackFight.AfterUpdate subData_after_update = CreatePBPackFightAfterUpdate(subT_after_update);
				data.after_update.Add(subData_after_update);
			}
			else
			{
				break;
			}
		}
	}
	if( t.ContainsKey<string>("buffer"))
	{
		LuaTable subTable_buffer = t.Get<string, LuaTable>("buffer");
		data.buffer = CreatePBPackBufferBufferChange(subTable_buffer);
	}
	return data;
}
public LuaTable CreatePackFightFightUnit(PackFight.FightUnit data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("fight_sequence_id", data.fight_sequence_id);
	t.Set<string, int>("op", data.op);
	if (data.op_swap != null)
	{
		LuaTable t_FightOpSwap = CreatePackFightFightOpSwap(data.op_swap);
		t.Set<string, LuaTable>("op_swap", t_FightOpSwap);
	}
	if (data.op_supply != null)
	{
		LuaTable t_FightOpSupply = CreatePackFightFightOpSupply(data.op_supply);
		t.Set<string, LuaTable>("op_supply", t_FightOpSupply);
	}
	if (data.op_xipai != null)
	{
		LuaTable t_FightOpXipai = CreatePackFightFightOpXipai(data.op_xipai);
		t.Set<string, LuaTable>("op_xipai", t_FightOpXipai);
	}
	if (data.op_before != null)
	{
		LuaTable t_FightOpBefore = CreatePackFightFightOpBefore(data.op_before);
		t.Set<string, LuaTable>("op_before", t_FightOpBefore);
	}
	if (data.op_skill != null)
	{
		LuaTable t_FightOpUseSkill = CreatePackFightFightOpUseSkill(data.op_skill);
		t.Set<string, LuaTable>("op_skill", t_FightOpUseSkill);
	}
	if (data.op_after != null)
	{
		LuaTable t_FightOpAfter = CreatePackFightFightOpAfter(data.op_after);
		t.Set<string, LuaTable>("op_after", t_FightOpAfter);
	}
	if (data.error != null)
	{
		LuaTable t_FightError = CreatePackFightFightError(data.error);
		t.Set<string, LuaTable>("error", t_FightError);
	}
	if (data.objs_update != null)
	{
		LuaTable objs_update_item = luaEnv.NewTable();
		for(int i = 0;i < data.objs_update.Count; ++i)
		{
			LuaTable t_objs_update = CreatePackFightFightObjectUpdate(data.objs_update[i]);
			objs_update_item.Set<int, LuaTable>(i+1, t_objs_update);
		}
		t.Set<string, LuaTable>("objs_update", objs_update_item);
	}
	return t;
}
public PackFight.FightUnit CreatePBPackFightFightUnit(LuaTable t)
{
	PackFight.FightUnit data = new PackFight.FightUnit();
	if( t.ContainsKey<string>("fight_sequence_id"))
	{
		data.fight_sequence_id = t.Get<string, int>("fight_sequence_id");
	}
	else
	{
		Log.LogError("Field fight_sequence_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("op"))
	{
		data.op = t.Get<string, int>("op");
	}
	else
	{
		Log.LogError("Field op Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("op_swap"))
	{
		LuaTable subTable_op_swap = t.Get<string, LuaTable>("op_swap");
		data.op_swap = CreatePBPackFightFightOpSwap(subTable_op_swap);
	}
	if( t.ContainsKey<string>("op_supply"))
	{
		LuaTable subTable_op_supply = t.Get<string, LuaTable>("op_supply");
		data.op_supply = CreatePBPackFightFightOpSupply(subTable_op_supply);
	}
	if( t.ContainsKey<string>("op_xipai"))
	{
		LuaTable subTable_op_xipai = t.Get<string, LuaTable>("op_xipai");
		data.op_xipai = CreatePBPackFightFightOpXipai(subTable_op_xipai);
	}
	if( t.ContainsKey<string>("op_before"))
	{
		LuaTable subTable_op_before = t.Get<string, LuaTable>("op_before");
		data.op_before = CreatePBPackFightFightOpBefore(subTable_op_before);
	}
	if( t.ContainsKey<string>("op_skill"))
	{
		LuaTable subTable_op_skill = t.Get<string, LuaTable>("op_skill");
		data.op_skill = CreatePBPackFightFightOpUseSkill(subTable_op_skill);
	}
	if( t.ContainsKey<string>("op_after"))
	{
		LuaTable subTable_op_after = t.Get<string, LuaTable>("op_after");
		data.op_after = CreatePBPackFightFightOpAfter(subTable_op_after);
	}
	if( t.ContainsKey<string>("error"))
	{
		LuaTable subTable_error = t.Get<string, LuaTable>("error");
		data.error = CreatePBPackFightFightError(subTable_error);
	}
	if(t.ContainsKey("objs_update"))
	{
		LuaTable t_objs_update = t.Get<string, LuaTable>("objs_update");
		data.objs_update = new List<PackFight.FightObjectUpdate>();
		for(int i = 0; ;++i)
		{
			if(t_objs_update.ContainsKey<int>(i + 1))
			{
				LuaTable subT_objs_update = t_objs_update.Get<int, LuaTable>(i + 1);
				PackFight.FightObjectUpdate subData_objs_update = CreatePBPackFightFightObjectUpdate(subT_objs_update);
				data.objs_update.Add(subData_objs_update);
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightFightError(PackFight.FightError data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("err_id", data.err_id);
	if (data.err_params != null)
	{
		LuaTable err_params_item = luaEnv.NewTable();
		for(int i = 0;i < data.err_params.Count; ++i)
		{
			err_params_item.Set<int, string>(i+1, data.err_params[i]);
		}
		t.Set<string, LuaTable>("err_params", err_params_item);
	}
	return t;
}
public PackFight.FightError CreatePBPackFightFightError(LuaTable t)
{
	PackFight.FightError data = new PackFight.FightError();
	if( t.ContainsKey<string>("err_id"))
	{
		data.err_id = t.Get<string, int>("err_id");
	}
	else
	{
		Log.LogError("Field err_id Not Exist in LuaTable From Service!!");
	}
	if(t.ContainsKey("err_params"))
	{
		LuaTable t_err_params = t.Get<string, LuaTable>("err_params");
		data.err_params = new List<string>();
		for(int i = 0; ;++i)
		{
			if(t_err_params.ContainsKey<int>(i + 1))
			{
				data.err_params.Add(t_err_params.Get<int, string>(i + 1));
			}
			else
			{
				break;
			}
		}
	}
	return data;
}
public LuaTable CreatePackFightC2M_GAME_START(PackFight.C2M_GAME_START data)
{
	LuaTable t = luaEnv.NewTable();
	if (data.client != null)
	{
		LuaTable t_M2C_FIGHT_OP_FIN = CreatePackFightM2C_FIGHT_OP_FIN(data.client);
		t.Set<string, LuaTable>("client", t_M2C_FIGHT_OP_FIN);
	}
	return t;
}
public PackFight.C2M_GAME_START CreatePBPackFightC2M_GAME_START(LuaTable t)
{
	PackFight.C2M_GAME_START data = new PackFight.C2M_GAME_START();
	if( t.ContainsKey<string>("client"))
	{
		LuaTable subTable_client = t.Get<string, LuaTable>("client");
		data.client = CreatePBPackFightM2C_FIGHT_OP_FIN(subTable_client);
	}
	return data;
}
public LuaTable CreatePackFightC2M_FIGHT_CHECK(PackFight.C2M_FIGHT_CHECK data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("packet_seq_id", data.packet_seq_id);
	LuaTable t_FightUnit = CreatePackFightFightUnit(data.op_unit);
	t.Set<string, LuaTable>("op_unit", t_FightUnit);
	if (data.client != null)
	{
		LuaTable t_M2C_FIGHT_CHECK = CreatePackFightM2C_FIGHT_CHECK(data.client);
		t.Set<string, LuaTable>("client", t_M2C_FIGHT_CHECK);
	}
	return t;
}
public PackFight.C2M_FIGHT_CHECK CreatePBPackFightC2M_FIGHT_CHECK(LuaTable t)
{
	PackFight.C2M_FIGHT_CHECK data = new PackFight.C2M_FIGHT_CHECK();
	if( t.ContainsKey<string>("packet_seq_id"))
	{
		data.packet_seq_id = t.Get<string, int>("packet_seq_id");
	}
	else
	{
		Log.LogError("Field packet_seq_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("op_unit"))
	{
		LuaTable subTable_op_unit = t.Get<string, LuaTable>("op_unit");
		data.op_unit = CreatePBPackFightFightUnit(subTable_op_unit);
	}
	else
	{
		Log.LogError("Field op_unit Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("client"))
	{
		LuaTable subTable_client = t.Get<string, LuaTable>("client");
		data.client = CreatePBPackFightM2C_FIGHT_CHECK(subTable_client);
	}
	return data;
}
public LuaTable CreatePackFightM2C_FIGHT_CHECK(PackFight.M2C_FIGHT_CHECK data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("packet_seq_id", data.packet_seq_id);
	LuaTable t_FightUnit = CreatePackFightFightUnit(data.op_unit);
	t.Set<string, LuaTable>("op_unit", t_FightUnit);
	return t;
}
public PackFight.M2C_FIGHT_CHECK CreatePBPackFightM2C_FIGHT_CHECK(LuaTable t)
{
	PackFight.M2C_FIGHT_CHECK data = new PackFight.M2C_FIGHT_CHECK();
	if( t.ContainsKey<string>("packet_seq_id"))
	{
		data.packet_seq_id = t.Get<string, int>("packet_seq_id");
	}
	else
	{
		Log.LogError("Field packet_seq_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("op_unit"))
	{
		LuaTable subTable_op_unit = t.Get<string, LuaTable>("op_unit");
		data.op_unit = CreatePBPackFightFightUnit(subTable_op_unit);
	}
	else
	{
		Log.LogError("Field op_unit Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackFightC2M_FIGHT_USE_SKILL(PackFight.C2M_FIGHT_USE_SKILL data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("packet_seq_id", data.packet_seq_id);
	t.Set<string, int>("skill_id", data.skill_id);
	t.Set<string, int>("fighter_aoi_id", data.fighter_aoi_id);
	if (data.attacked_aoi_ids != null)
	{
		LuaTable attacked_aoi_ids_item = luaEnv.NewTable();
		for(int i = 0;i < data.attacked_aoi_ids.Count; ++i)
		{
			attacked_aoi_ids_item.Set<int, int>(i+1, data.attacked_aoi_ids[i]);
		}
		t.Set<string, LuaTable>("attacked_aoi_ids", attacked_aoi_ids_item);
	}
	if (data.client != null)
	{
		LuaTable t_M2C_FIGHT_USE_SKILL = CreatePackFightM2C_FIGHT_USE_SKILL(data.client);
		t.Set<string, LuaTable>("client", t_M2C_FIGHT_USE_SKILL);
	}
	return t;
}
public PackFight.C2M_FIGHT_USE_SKILL CreatePBPackFightC2M_FIGHT_USE_SKILL(LuaTable t)
{
	PackFight.C2M_FIGHT_USE_SKILL data = new PackFight.C2M_FIGHT_USE_SKILL();
	if( t.ContainsKey<string>("packet_seq_id"))
	{
		data.packet_seq_id = t.Get<string, int>("packet_seq_id");
	}
	else
	{
		Log.LogError("Field packet_seq_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("skill_id"))
	{
		data.skill_id = t.Get<string, int>("skill_id");
	}
	else
	{
		Log.LogError("Field skill_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("fighter_aoi_id"))
	{
		data.fighter_aoi_id = t.Get<string, int>("fighter_aoi_id");
	}
	else
	{
		Log.LogError("Field fighter_aoi_id Not Exist in LuaTable From Service!!");
	}
	if(t.ContainsKey("attacked_aoi_ids"))
	{
		LuaTable t_attacked_aoi_ids = t.Get<string, LuaTable>("attacked_aoi_ids");
		data.attacked_aoi_ids = new List<int>();
		for(int i = 0; ;++i)
		{
			if(t_attacked_aoi_ids.ContainsKey<int>(i + 1))
			{
				data.attacked_aoi_ids.Add(t_attacked_aoi_ids.Get<int, int>(i + 1));
			}
			else
			{
				break;
			}
		}
	}
	if( t.ContainsKey<string>("client"))
	{
		LuaTable subTable_client = t.Get<string, LuaTable>("client");
		data.client = CreatePBPackFightM2C_FIGHT_USE_SKILL(subTable_client);
	}
	return data;
}
public LuaTable CreatePackFightM2C_FIGHT_USE_SKILL(PackFight.M2C_FIGHT_USE_SKILL data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("packet_seq_id", data.packet_seq_id);
	if (data.fight_result != null)
	{
		LuaTable t_FightUnit = CreatePackFightFightUnit(data.fight_result);
		t.Set<string, LuaTable>("fight_result", t_FightUnit);
	}
	return t;
}
public PackFight.M2C_FIGHT_USE_SKILL CreatePBPackFightM2C_FIGHT_USE_SKILL(LuaTable t)
{
	PackFight.M2C_FIGHT_USE_SKILL data = new PackFight.M2C_FIGHT_USE_SKILL();
	if( t.ContainsKey<string>("packet_seq_id"))
	{
		data.packet_seq_id = t.Get<string, int>("packet_seq_id");
	}
	else
	{
		Log.LogError("Field packet_seq_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("fight_result"))
	{
		LuaTable subTable_fight_result = t.Get<string, LuaTable>("fight_result");
		data.fight_result = CreatePBPackFightFightUnit(subTable_fight_result);
	}
	return data;
}
public LuaTable CreatePackFightC2M_FIGHT_OP_FIN(PackFight.C2M_FIGHT_OP_FIN data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("packet_seq_id", data.packet_seq_id);
	if (data.client != null)
	{
		LuaTable t_M2C_FIGHT_OP_FIN = CreatePackFightM2C_FIGHT_OP_FIN(data.client);
		t.Set<string, LuaTable>("client", t_M2C_FIGHT_OP_FIN);
	}
	return t;
}
public PackFight.C2M_FIGHT_OP_FIN CreatePBPackFightC2M_FIGHT_OP_FIN(LuaTable t)
{
	PackFight.C2M_FIGHT_OP_FIN data = new PackFight.C2M_FIGHT_OP_FIN();
	if( t.ContainsKey<string>("packet_seq_id"))
	{
		data.packet_seq_id = t.Get<string, int>("packet_seq_id");
	}
	else
	{
		Log.LogError("Field packet_seq_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("client"))
	{
		LuaTable subTable_client = t.Get<string, LuaTable>("client");
		data.client = CreatePBPackFightM2C_FIGHT_OP_FIN(subTable_client);
	}
	return data;
}
public LuaTable CreatePackFightM2C_FIGHT_OP_FIN(PackFight.M2C_FIGHT_OP_FIN data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("packet_seq_id", data.packet_seq_id);
	if (data.my_after != null)
	{
		LuaTable t_FightUnit = CreatePackFightFightUnit(data.my_after);
		t.Set<string, LuaTable>("my_after", t_FightUnit);
	}
	if (data.enemy_before != null)
	{
		LuaTable t_FightUnit = CreatePackFightFightUnit(data.enemy_before);
		t.Set<string, LuaTable>("enemy_before", t_FightUnit);
	}
	if (data.enemy_use_skill != null)
	{
		LuaTable enemy_use_skill_item = luaEnv.NewTable();
		for(int i = 0;i < data.enemy_use_skill.Count; ++i)
		{
			LuaTable t_enemy_use_skill = CreatePackFightFightUnit(data.enemy_use_skill[i]);
			enemy_use_skill_item.Set<int, LuaTable>(i+1, t_enemy_use_skill);
		}
		t.Set<string, LuaTable>("enemy_use_skill", enemy_use_skill_item);
	}
	if (data.enemy_after != null)
	{
		LuaTable t_FightUnit = CreatePackFightFightUnit(data.enemy_after);
		t.Set<string, LuaTable>("enemy_after", t_FightUnit);
	}
	if (data.my_before != null)
	{
		LuaTable t_FightUnit = CreatePackFightFightUnit(data.my_before);
		t.Set<string, LuaTable>("my_before", t_FightUnit);
	}
	t.Set<string, bool>("is_next_wave", data.is_next_wave);
	return t;
}
public PackFight.M2C_FIGHT_OP_FIN CreatePBPackFightM2C_FIGHT_OP_FIN(LuaTable t)
{
	PackFight.M2C_FIGHT_OP_FIN data = new PackFight.M2C_FIGHT_OP_FIN();
	if( t.ContainsKey<string>("packet_seq_id"))
	{
		data.packet_seq_id = t.Get<string, int>("packet_seq_id");
	}
	else
	{
		Log.LogError("Field packet_seq_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("my_after"))
	{
		LuaTable subTable_my_after = t.Get<string, LuaTable>("my_after");
		data.my_after = CreatePBPackFightFightUnit(subTable_my_after);
	}
	if( t.ContainsKey<string>("enemy_before"))
	{
		LuaTable subTable_enemy_before = t.Get<string, LuaTable>("enemy_before");
		data.enemy_before = CreatePBPackFightFightUnit(subTable_enemy_before);
	}
	if(t.ContainsKey("enemy_use_skill"))
	{
		LuaTable t_enemy_use_skill = t.Get<string, LuaTable>("enemy_use_skill");
		data.enemy_use_skill = new List<PackFight.FightUnit>();
		for(int i = 0; ;++i)
		{
			if(t_enemy_use_skill.ContainsKey<int>(i + 1))
			{
				LuaTable subT_enemy_use_skill = t_enemy_use_skill.Get<int, LuaTable>(i + 1);
				PackFight.FightUnit subData_enemy_use_skill = CreatePBPackFightFightUnit(subT_enemy_use_skill);
				data.enemy_use_skill.Add(subData_enemy_use_skill);
			}
			else
			{
				break;
			}
		}
	}
	if( t.ContainsKey<string>("enemy_after"))
	{
		LuaTable subTable_enemy_after = t.Get<string, LuaTable>("enemy_after");
		data.enemy_after = CreatePBPackFightFightUnit(subTable_enemy_after);
	}
	if( t.ContainsKey<string>("my_before"))
	{
		LuaTable subTable_my_before = t.Get<string, LuaTable>("my_before");
		data.my_before = CreatePBPackFightFightUnit(subTable_my_before);
	}
	if( t.ContainsKey<string>("is_next_wave"))
	{
		data.is_next_wave = t.Get<string, bool>("is_next_wave");
	}
	else
	{
		Log.LogError("Field is_next_wave Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackFightC2M_FIGHT_NEXT_WAVE(PackFight.C2M_FIGHT_NEXT_WAVE data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("packet_seq_id", data.packet_seq_id);
	if (data.client != null)
	{
		LuaTable t_M2C_FIGHT_OP_FIN = CreatePackFightM2C_FIGHT_OP_FIN(data.client);
		t.Set<string, LuaTable>("client", t_M2C_FIGHT_OP_FIN);
	}
	return t;
}
public PackFight.C2M_FIGHT_NEXT_WAVE CreatePBPackFightC2M_FIGHT_NEXT_WAVE(LuaTable t)
{
	PackFight.C2M_FIGHT_NEXT_WAVE data = new PackFight.C2M_FIGHT_NEXT_WAVE();
	if( t.ContainsKey<string>("packet_seq_id"))
	{
		data.packet_seq_id = t.Get<string, int>("packet_seq_id");
	}
	else
	{
		Log.LogError("Field packet_seq_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("client"))
	{
		LuaTable subTable_client = t.Get<string, LuaTable>("client");
		data.client = CreatePBPackFightM2C_FIGHT_OP_FIN(subTable_client);
	}
	return data;
}
public LuaTable CreatePackFightC2M_FIGHT_FINISH(PackFight.C2M_FIGHT_FINISH data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("packet_seq_id", data.packet_seq_id);
	t.Set<string, bool>("is_win", data.is_win);
	return t;
}
public PackFight.C2M_FIGHT_FINISH CreatePBPackFightC2M_FIGHT_FINISH(LuaTable t)
{
	PackFight.C2M_FIGHT_FINISH data = new PackFight.C2M_FIGHT_FINISH();
	if( t.ContainsKey<string>("packet_seq_id"))
	{
		data.packet_seq_id = t.Get<string, int>("packet_seq_id");
	}
	else
	{
		Log.LogError("Field packet_seq_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("is_win"))
	{
		data.is_win = t.Get<string, bool>("is_win");
	}
	else
	{
		Log.LogError("Field is_win Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackFightM2C_FIGHT_FINISH(PackFight.M2C_FIGHT_FINISH data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("packet_seq_id", data.packet_seq_id);
	t.Set<string, bool>("is_win", data.is_win);
	return t;
}
public PackFight.M2C_FIGHT_FINISH CreatePBPackFightM2C_FIGHT_FINISH(LuaTable t)
{
	PackFight.M2C_FIGHT_FINISH data = new PackFight.M2C_FIGHT_FINISH();
	if( t.ContainsKey<string>("packet_seq_id"))
	{
		data.packet_seq_id = t.Get<string, int>("packet_seq_id");
	}
	else
	{
		Log.LogError("Field packet_seq_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("is_win"))
	{
		data.is_win = t.Get<string, bool>("is_win");
	}
	else
	{
		Log.LogError("Field is_win Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackFightWave(PackFight.Wave data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("wave_id", data.wave_id);
	if (data.enemies != null)
	{
		LuaTable enemies_item = luaEnv.NewTable();
		for(int i = 0;i < data.enemies.Count; ++i)
		{
			enemies_item.Set<int, int>(i+1, data.enemies[i]);
		}
		t.Set<string, LuaTable>("enemies", enemies_item);
	}
	t.Set<string, bool>("is_boss", data.is_boss);
	t.Set<string, bool>("is_first_hand", data.is_first_hand);
	return t;
}
public PackFight.Wave CreatePBPackFightWave(LuaTable t)
{
	PackFight.Wave data = new PackFight.Wave();
	if( t.ContainsKey<string>("wave_id"))
	{
		data.wave_id = t.Get<string, int>("wave_id");
	}
	else
	{
		Log.LogError("Field wave_id Not Exist in LuaTable From Service!!");
	}
	if(t.ContainsKey("enemies"))
	{
		LuaTable t_enemies = t.Get<string, LuaTable>("enemies");
		data.enemies = new List<int>();
		for(int i = 0; ;++i)
		{
			if(t_enemies.ContainsKey<int>(i + 1))
			{
				data.enemies.Add(t_enemies.Get<int, int>(i + 1));
			}
			else
			{
				break;
			}
		}
	}
	if( t.ContainsKey<string>("is_boss"))
	{
		data.is_boss = t.Get<string, bool>("is_boss");
	}
	else
	{
		Log.LogError("Field is_boss Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("is_first_hand"))
	{
		data.is_first_hand = t.Get<string, bool>("is_first_hand");
	}
	else
	{
		Log.LogError("Field is_first_hand Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackFightPluzzlesItem(PackFight.PluzzlesItem data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("zhu_position", data.zhu_position);
	t.Set<string, int>("color", data.color);
	return t;
}
public PackFight.PluzzlesItem CreatePBPackFightPluzzlesItem(LuaTable t)
{
	PackFight.PluzzlesItem data = new PackFight.PluzzlesItem();
	if( t.ContainsKey<string>("zhu_position"))
	{
		data.zhu_position = t.Get<string, int>("zhu_position");
	}
	else
	{
		Log.LogError("Field zhu_position Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("color"))
	{
		data.color = t.Get<string, int>("color");
	}
	else
	{
		Log.LogError("Field color Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackFightM2C_FIGHT_INFO(PackFight.M2C_FIGHT_INFO data)
{
	LuaTable t = luaEnv.NewTable();
	if (data.objs != null)
	{
		LuaTable objs_item = luaEnv.NewTable();
		for(int i = 0;i < data.objs.Count; ++i)
		{
			LuaTable t_objs = CreatePackFightFightObject(data.objs[i]);
			objs_item.Set<int, LuaTable>(i+1, t_objs);
		}
		t.Set<string, LuaTable>("objs", objs_item);
	}
	if (data.items != null)
	{
		LuaTable items_item = luaEnv.NewTable();
		for(int i = 0;i < data.items.Count; ++i)
		{
			LuaTable t_items = CreatePackFightPluzzlesItem(data.items[i]);
			items_item.Set<int, LuaTable>(i+1, t_items);
		}
		t.Set<string, LuaTable>("items", items_item);
	}
	if (data.waves != null)
	{
		LuaTable waves_item = luaEnv.NewTable();
		for(int i = 0;i < data.waves.Count; ++i)
		{
			LuaTable t_waves = CreatePackFightWave(data.waves[i]);
			waves_item.Set<int, LuaTable>(i+1, t_waves);
		}
		t.Set<string, LuaTable>("waves", waves_item);
	}
	if (data.heros != null)
	{
		LuaTable heros_item = luaEnv.NewTable();
		for(int i = 0;i < data.heros.Count; ++i)
		{
			heros_item.Set<int, int>(i+1, data.heros[i]);
		}
		t.Set<string, LuaTable>("heros", heros_item);
	}
	t.Set<string, int>("now_wave", data.now_wave);
	t.Set<string, int>("last_op", data.last_op);
	t.Set<string, System.Int64>("skill_random", data.skill_random);
	t.Set<string, System.Int64>("supply_random", data.supply_random);
	t.Set<string, int>("packet_seq_id", data.packet_seq_id);
	t.Set<string, int>("obj_id_start", data.obj_id_start);
	t.Set<string, int>("fight_sequence_id", data.fight_sequence_id);
	return t;
}
public PackFight.M2C_FIGHT_INFO CreatePBPackFightM2C_FIGHT_INFO(LuaTable t)
{
	PackFight.M2C_FIGHT_INFO data = new PackFight.M2C_FIGHT_INFO();
	if(t.ContainsKey("objs"))
	{
		LuaTable t_objs = t.Get<string, LuaTable>("objs");
		data.objs = new List<PackFight.FightObject>();
		for(int i = 0; ;++i)
		{
			if(t_objs.ContainsKey<int>(i + 1))
			{
				LuaTable subT_objs = t_objs.Get<int, LuaTable>(i + 1);
				PackFight.FightObject subData_objs = CreatePBPackFightFightObject(subT_objs);
				data.objs.Add(subData_objs);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("items"))
	{
		LuaTable t_items = t.Get<string, LuaTable>("items");
		data.items = new List<PackFight.PluzzlesItem>();
		for(int i = 0; ;++i)
		{
			if(t_items.ContainsKey<int>(i + 1))
			{
				LuaTable subT_items = t_items.Get<int, LuaTable>(i + 1);
				PackFight.PluzzlesItem subData_items = CreatePBPackFightPluzzlesItem(subT_items);
				data.items.Add(subData_items);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("waves"))
	{
		LuaTable t_waves = t.Get<string, LuaTable>("waves");
		data.waves = new List<PackFight.Wave>();
		for(int i = 0; ;++i)
		{
			if(t_waves.ContainsKey<int>(i + 1))
			{
				LuaTable subT_waves = t_waves.Get<int, LuaTable>(i + 1);
				PackFight.Wave subData_waves = CreatePBPackFightWave(subT_waves);
				data.waves.Add(subData_waves);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("heros"))
	{
		LuaTable t_heros = t.Get<string, LuaTable>("heros");
		data.heros = new List<int>();
		for(int i = 0; ;++i)
		{
			if(t_heros.ContainsKey<int>(i + 1))
			{
				data.heros.Add(t_heros.Get<int, int>(i + 1));
			}
			else
			{
				break;
			}
		}
	}
	if( t.ContainsKey<string>("now_wave"))
	{
		data.now_wave = t.Get<string, int>("now_wave");
	}
	else
	{
		Log.LogError("Field now_wave Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("last_op"))
	{
		data.last_op = t.Get<string, int>("last_op");
	}
	else
	{
		Log.LogError("Field last_op Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("skill_random"))
	{
		data.skill_random = t.Get<string, System.Int64>("skill_random");
	}
	else
	{
		Log.LogError("Field skill_random Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("supply_random"))
	{
		data.supply_random = t.Get<string, System.Int64>("supply_random");
	}
	else
	{
		Log.LogError("Field supply_random Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("packet_seq_id"))
	{
		data.packet_seq_id = t.Get<string, int>("packet_seq_id");
	}
	else
	{
		Log.LogError("Field packet_seq_id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("obj_id_start"))
	{
		data.obj_id_start = t.Get<string, int>("obj_id_start");
	}
	else
	{
		Log.LogError("Field obj_id_start Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("fight_sequence_id"))
	{
		data.fight_sequence_id = t.Get<string, int>("fight_sequence_id");
	}
	else
	{
		Log.LogError("Field fight_sequence_id Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackRetinueRetinue(PackRetinue.Retinue data)
{
	LuaTable t = luaEnv.NewTable();
	t.Set<string, int>("id", data.id);
	t.Set<string, int>("hp", data.hp);
	t.Set<string, int>("defence", data.defence);
	t.Set<string, int>("life", data.life);
	t.Set<string, int>("max_hp", data.max_hp);
	t.Set<string, int>("host_aoi_id", data.host_aoi_id);
	return t;
}
public PackRetinue.Retinue CreatePBPackRetinueRetinue(LuaTable t)
{
	PackRetinue.Retinue data = new PackRetinue.Retinue();
	if( t.ContainsKey<string>("id"))
	{
		data.id = t.Get<string, int>("id");
	}
	else
	{
		Log.LogError("Field id Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("hp"))
	{
		data.hp = t.Get<string, int>("hp");
	}
	else
	{
		Log.LogError("Field hp Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("defence"))
	{
		data.defence = t.Get<string, int>("defence");
	}
	else
	{
		Log.LogError("Field defence Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("life"))
	{
		data.life = t.Get<string, int>("life");
	}
	else
	{
		Log.LogError("Field life Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("max_hp"))
	{
		data.max_hp = t.Get<string, int>("max_hp");
	}
	else
	{
		Log.LogError("Field max_hp Not Exist in LuaTable From Service!!");
	}
	if( t.ContainsKey<string>("host_aoi_id"))
	{
		data.host_aoi_id = t.Get<string, int>("host_aoi_id");
	}
	else
	{
		Log.LogError("Field host_aoi_id Not Exist in LuaTable From Service!!");
	}
	return data;
}
public LuaTable CreatePackRetinueRetinueChange(PackRetinue.RetinueChange data)
{
	LuaTable t = luaEnv.NewTable();
	if (data.insert_retinues != null)
	{
		LuaTable insert_retinues_item = luaEnv.NewTable();
		for(int i = 0;i < data.insert_retinues.Count; ++i)
		{
			LuaTable t_insert_retinues = CreatePackRetinueRetinue(data.insert_retinues[i]);
			insert_retinues_item.Set<int, LuaTable>(i+1, t_insert_retinues);
		}
		t.Set<string, LuaTable>("insert_retinues", insert_retinues_item);
	}
	if (data.remove_retinues != null)
	{
		LuaTable remove_retinues_item = luaEnv.NewTable();
		for(int i = 0;i < data.remove_retinues.Count; ++i)
		{
			remove_retinues_item.Set<int, int>(i+1, data.remove_retinues[i]);
		}
		t.Set<string, LuaTable>("remove_retinues", remove_retinues_item);
	}
	if (data.update_retinues != null)
	{
		LuaTable update_retinues_item = luaEnv.NewTable();
		for(int i = 0;i < data.update_retinues.Count; ++i)
		{
			LuaTable t_update_retinues = CreatePackRetinueRetinue(data.update_retinues[i]);
			update_retinues_item.Set<int, LuaTable>(i+1, t_update_retinues);
		}
		t.Set<string, LuaTable>("update_retinues", update_retinues_item);
	}
	return t;
}
public PackRetinue.RetinueChange CreatePBPackRetinueRetinueChange(LuaTable t)
{
	PackRetinue.RetinueChange data = new PackRetinue.RetinueChange();
	if(t.ContainsKey("insert_retinues"))
	{
		LuaTable t_insert_retinues = t.Get<string, LuaTable>("insert_retinues");
		data.insert_retinues = new List<PackRetinue.Retinue>();
		for(int i = 0; ;++i)
		{
			if(t_insert_retinues.ContainsKey<int>(i + 1))
			{
				LuaTable subT_insert_retinues = t_insert_retinues.Get<int, LuaTable>(i + 1);
				PackRetinue.Retinue subData_insert_retinues = CreatePBPackRetinueRetinue(subT_insert_retinues);
				data.insert_retinues.Add(subData_insert_retinues);
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("remove_retinues"))
	{
		LuaTable t_remove_retinues = t.Get<string, LuaTable>("remove_retinues");
		data.remove_retinues = new List<int>();
		for(int i = 0; ;++i)
		{
			if(t_remove_retinues.ContainsKey<int>(i + 1))
			{
				data.remove_retinues.Add(t_remove_retinues.Get<int, int>(i + 1));
			}
			else
			{
				break;
			}
		}
	}
	if(t.ContainsKey("update_retinues"))
	{
		LuaTable t_update_retinues = t.Get<string, LuaTable>("update_retinues");
		data.update_retinues = new List<PackRetinue.Retinue>();
		for(int i = 0; ;++i)
		{
			if(t_update_retinues.ContainsKey<int>(i + 1))
			{
				LuaTable subT_update_retinues = t_update_retinues.Get<int, LuaTable>(i + 1);
				PackRetinue.Retinue subData_update_retinues = CreatePBPackRetinueRetinue(subT_update_retinues);
				data.update_retinues.Add(subData_update_retinues);
			}
			else
			{
				break;
			}
		}
	}
	return data;
}

}